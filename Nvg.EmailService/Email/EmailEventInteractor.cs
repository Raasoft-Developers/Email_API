using EventBus.Abstractions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailHistory;
using Nvg.EmailService.EmailProvider;
using Nvg.EmailService.EmailQuota;
using Nvg.EmailService.EmailServiceProviders;
using Nvg.EmailService.EmailTemplate;
using Nvg.EmailService.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailEventInteractor : IEmailEventInteractor
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<EmailEventInteractor> _logger;
        private readonly IEmailProviderInteractor _emailProviderService;
        private readonly IConfiguration _configuration;
        private readonly IEmailTemplateInteractor _emailTemplateInteractor;
        private readonly IEmailQuotaInteractor _emailQuotaInteractor;
        private readonly IEmailHistoryInteractor _emailHistoryInteractor;
        private readonly ILogger<SMTPProvider> _smtpLogger;
        private readonly ILogger<SendGridProvider> _sendgridLogger;
        private EmailProviderConnectionString _emailProviderConnectionString;

        //public EmailEventInteractor(IEventBus eventBus, ILogger<EmailEventInteractor> logger)
        //{
        //    _eventBus = eventBus;
        //    _logger = logger;
        //}

        // This constructor is used when EmailEventBusEnabled = true in appsettings
        public EmailEventInteractor(IEventBus eventBus, ILogger<EmailEventInteractor> logger, IEmailProviderInteractor emailProviderService, IConfiguration configuration,
            IEmailTemplateInteractor emailTemplateInteractor, IEmailQuotaInteractor emailQuotaInteractor,
            IEmailHistoryInteractor emailHistoryInteractor, ILogger<SMTPProvider> smtpLogger, ILogger<SendGridProvider> sendgridLogger)
        {
            _eventBus = eventBus;
            _logger = logger;
            _emailProviderService = emailProviderService;
            _configuration = configuration;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailQuotaInteractor = emailQuotaInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _smtpLogger = smtpLogger;
            _sendgridLogger = sendgridLogger;
        }

        // This constructor is used when EmailEventBusEnabled = false in appsettings
        public EmailEventInteractor(ILogger<EmailEventInteractor> logger, IEmailProviderInteractor emailProviderService, IConfiguration configuration,
           IEmailTemplateInteractor emailTemplateInteractor, IEmailQuotaInteractor emailQuotaInteractor,
           IEmailHistoryInteractor emailHistoryInteractor, ILogger<SMTPProvider> smtpLogger, ILogger<SendGridProvider> sendgridLogger)
        {
            _logger = logger;
            _emailProviderService = emailProviderService;
            _configuration = configuration;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailQuotaInteractor = emailQuotaInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _smtpLogger = smtpLogger;
            _sendgridLogger = sendgridLogger;
        }

        public void SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation($"SendMail method hit");
            _logger.LogInformation($"Channel Key: {emailInputs.ChannelKey} , Template Name: {emailInputs.TemplateName}, Variant: {emailInputs.Variant}, Recipients: {emailInputs.Recipients}, Sender: {emailInputs.Sender}.");
            //string user = (!string.IsNullOrEmpty(emailInputs.Username) ? emailInputs.Username : emailInputs.Recipients);
            var isEventBusEnabled = _configuration.GetValue<bool>("EmailEventBusEnabled");
            _logger.LogDebug($"Is Event Bus enabled : {isEventBusEnabled}");
            if (isEventBusEnabled)
            {
                var sendEmailEvent = new SendEmailEvent();
                sendEmailEvent.ChannelKey = emailInputs.ChannelKey;
                sendEmailEvent.TemplateName = emailInputs.TemplateName;
                sendEmailEvent.Variant = emailInputs.Variant;
                sendEmailEvent.Sender = emailInputs.Sender;
                sendEmailEvent.Recipients = emailInputs.Recipients;
                sendEmailEvent.Subject = emailInputs.Subject;
                sendEmailEvent.MessageParts = emailInputs.MessageParts;
                sendEmailEvent.Tag = emailInputs.Tag;
                _logger.LogInformation("Publishing Email data.");
                _eventBus.Publish(sendEmailEvent);
            }
            else
            {
                _logger.LogInformation("***** GetEmailProviderByChannel *****");
                var provider = _emailProviderService.GetEmailProviderByChannel(emailInputs.ChannelKey)?.Result;
                _emailProviderConnectionString = new EmailProviderConnectionString(provider.Configuration);
                string sender;
                _logger.LogInformation("***** GetEmailProvider *****");
                IEmailProvider emailProvider = GetEmailProvider(provider.Type);
                _logger.LogDebug($"Provider Name: {provider.Name}");

                var email = new EmailSenderModel.Email
                {
                    Recipients = emailInputs.Recipients,
                    Sender = emailInputs.Sender,
                    TemplateName = emailInputs.TemplateName,
                    Variant = emailInputs.Variant,
                    ChannelKey = emailInputs.ChannelKey,
                    MessageParts = emailInputs.MessageParts,
                    ProviderName = provider.Name,
                    Subject = emailInputs.Subject,
                    Tag = emailInputs.Tag
                };
                if (string.IsNullOrEmpty(email.Sender))
                {
                    _logger.LogInformation("***** GetSender *****");
                    sender = email.GetSender(_emailTemplateInteractor);
                }
                else
                    sender = email.Sender;

                _logger.LogInformation("***** GetMessage *****");
                string message = email.GetMessage(_emailTemplateInteractor);
                _logger.LogInformation("***** SendEmail *****");
                var result = emailProvider.SendEmail(emailInputs.Recipients, message, emailInputs.Subject, sender).Result;

                if (string.IsNullOrEmpty(sender) && !string.IsNullOrEmpty(_emailProviderConnectionString.Fields["Sender"]))
                    sender = _emailProviderConnectionString.Fields["Sender"];

                _logger.LogInformation("***** AddEmailHistory and IncrementEmailQuota *****");
                foreach (var recipient in email.Recipients)
                {
                    var emailObj = new EmailHistoryDto()
                    {
                        MessageSent = message,
                        Sender = sender,
                        Recipients = recipient,
                        TemplateName = email.TemplateName,
                        TemplateVariant = email.Variant,
                        ChannelKey = email.ChannelKey,
                        ProviderName = email.ProviderName,
                        Tags = email.Tag,
                        SentOn = DateTime.UtcNow,
                        Status = result,
                        Attempts = 1,
                    };

                    _emailHistoryInteractor.AddEmailHistory(emailObj);
                    //_logger.LogInformation("***** IncrementEmailQuota *****");
                    _emailQuotaInteractor.IncrementEmailQuota(email.ChannelKey);
                }
                
            }
        }

        public void SendMailWithAttachment(EmailDto emailInputs)
        {
            _logger.LogInformation($"SendMailWithAttachments method hit");
            _logger.LogInformation($"Channel Key: {emailInputs.ChannelKey} , Template Name: {emailInputs.TemplateName}, Variant: {emailInputs.Variant}, Recipients: {emailInputs.Recipients}, Sender: {emailInputs.Sender}.");
            //string user = (!string.IsNullOrEmpty(emailInputs.Username) ? emailInputs.Username : emailInputs.Recipients);
            var isEventBusEnabled = _configuration.GetValue<bool>("EmailEventBusEnabled");
            _logger.LogDebug($"Is Event Bus enabled : {isEventBusEnabled}");
            if (isEventBusEnabled)
            {
                var sendEmailWithAttachmentEvent = new SendEmailWithAttachmentEvent();
                sendEmailWithAttachmentEvent.ChannelKey = emailInputs.ChannelKey;
                sendEmailWithAttachmentEvent.TemplateName = emailInputs.TemplateName;
                sendEmailWithAttachmentEvent.Variant = emailInputs.Variant;
                sendEmailWithAttachmentEvent.Sender = emailInputs.Sender;
                sendEmailWithAttachmentEvent.Recipients = emailInputs.Recipients;
                sendEmailWithAttachmentEvent.Subject = emailInputs.Subject;
                sendEmailWithAttachmentEvent.MessageParts = emailInputs.MessageParts;
                sendEmailWithAttachmentEvent.Files = emailInputs.Files;
                sendEmailWithAttachmentEvent.Tag = emailInputs.Tag;
                _logger.LogInformation("Publishing Email data.");
                _eventBus.Publish(sendEmailWithAttachmentEvent);
            }
            else
            {
                string sender;
                _logger.LogInformation("***** GetEmailProviderByChannel *****");
                var provider = _emailProviderService.GetEmailProviderByChannel(emailInputs.ChannelKey)?.Result;
                _emailProviderConnectionString = new EmailProviderConnectionString(provider.Configuration);
                _logger.LogInformation("***** GetEmailProvider *****");
                IEmailProvider emailProvider = GetEmailProvider(provider.Type);
                _logger.LogDebug($"Provider Name: {provider.Name}");
                /*if (emailSettings.EnableEmail)
                {*/
                var email = new EmailSenderModel.Email
                {
                    Recipients = emailInputs.Recipients,
                    Sender = emailInputs.Sender,
                    TemplateName = emailInputs.TemplateName,
                    Variant = emailInputs.Variant,
                    ChannelKey = emailInputs.ChannelKey,
                    MessageParts = emailInputs.MessageParts,
                    ProviderName = provider.Name,
                    Subject = emailInputs.Subject,
                    Tag = emailInputs.Tag,
                    Files = emailInputs.Files
                };

                if (string.IsNullOrEmpty(email.Sender))
                {
                    _logger.LogInformation("***** GetSender *****");
                    sender = email.GetSender(_emailTemplateInteractor);
                }
                else
                    sender = email.Sender;

                _logger.LogInformation("***** GetMessage *****");
                string message = email.GetMessage(_emailTemplateInteractor);
                _logger.LogInformation("***** SendEmailWithAttachments *****");
                var result = emailProvider.SendEmailWithAttachments(emailInputs.Recipients, emailInputs.Files, message, emailInputs.Subject, emailInputs.Sender).Result;

                if (string.IsNullOrEmpty(sender) && !string.IsNullOrEmpty(_emailProviderConnectionString.Fields["Sender"]))
                    sender = _emailProviderConnectionString.Fields["Sender"];

                _logger.LogInformation("***** AddEmailHistory and IncrementEmailQuota *****");
                foreach (var recipient in email.Recipients)
                {
                    var emailObj = new EmailHistoryDto()
                    {
                        MessageSent = message,
                        Sender = sender,
                        Recipients = recipient,
                        TemplateName = email.TemplateName,
                        TemplateVariant = email.Variant,
                        ChannelKey = email.ChannelKey,
                        ProviderName = email.ProviderName,
                        Tags = email.Tag,
                        SentOn = DateTime.UtcNow,
                        Status = result,
                        Attempts = 1,
                    };
                    _emailHistoryInteractor.AddEmailHistory(emailObj);

                    _emailQuotaInteractor.IncrementEmailQuota(email.ChannelKey);
                }               
            }
        }

        #region Private methods
        private IEmailProvider GetEmailProvider(string providerType)
        {
            _logger.LogInformation("GetEmailProvider method.");
            _logger.LogInformation("Getting appropriate Provider...");
            IEmailProvider emailProvider;

            switch (providerType.ToLower())
            {
                case "sendgrid":
                    emailProvider = new SendGridProvider(_emailProviderConnectionString, _sendgridLogger);
                    break;
                case "smtp":
                    emailProvider = new SMTPProvider(_emailProviderConnectionString, _smtpLogger);
                    break;
                default:
                    emailProvider = new SMTPProvider(_emailProviderConnectionString, _smtpLogger);
                    break;
            }
            _logger.LogInformation("Fetched appropriate Provider for " + providerType);
            return emailProvider;
        }
        #endregion
    }
}
