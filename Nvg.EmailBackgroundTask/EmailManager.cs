using Microsoft.Extensions.Logging;
using Nvg.EmailBackgroundTask.EmailProvider;
using Nvg.EmailBackgroundTask.Models;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailHistory;
using Nvg.EmailService.EmailQuota;
using Nvg.EmailService.EmailTemplate;
using System;

namespace Nvg.EmailBackgroundTask
{
    public class EmailManager
    {
        private readonly IEmailProvider _emailProvider;
        private readonly IEmailTemplateInteractor _emailTemplateInteractor;
        private readonly IEmailHistoryInteractor _emailHistoryInteractor;
        private readonly IEmailQuotaInteractor _emailQuotaInteractor;
        private readonly EmailProviderConnectionString _emailProviderConnectionString;
        private readonly ILogger _logger;

        public EmailManager(IEmailProvider emailProvider, IEmailTemplateInteractor emailTemplateInteractor, IEmailHistoryInteractor emailHistoryInteractor,
            IEmailQuotaInteractor emailQuotaInteractor, EmailProviderConnectionString emailProviderConnectionString, ILogger<EmailManager> logger)
        {
            _emailProvider = emailProvider;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _emailQuotaInteractor = emailQuotaInteractor;
            _emailProviderConnectionString = emailProviderConnectionString;
            _logger = logger;
        }

        /// <summary>
        /// Sends the email via the respective provider mentioned in provider settings and updates the history and quota tables.
        /// </summary>
        /// <param name="email"><see cref="Email"/> model</param>
        public void SendEmail(Email email)
        {
            _logger.LogInformation($"SendEmail method hit.");
            string sender = string.Empty;
            string message = email.GetMessage(_emailTemplateInteractor);
            _logger.LogInformation($"Message: {message}");
            // If external application didnot send the sender value, get it from template.
            if (string.IsNullOrEmpty(email.Sender))
                sender = email.GetSender(_emailTemplateInteractor);
            else
                sender = email.Sender;
            
            if (string.IsNullOrEmpty(sender))
                sender = _emailProviderConnectionString.Fields["Sender"];
            _logger.LogInformation($"Sender: {sender}");
            string emailResponseStatus = _emailProvider.SendEmail(email.Recipients, message, email.Subject, sender).Result;
            _logger.LogDebug($"Email response status: {emailResponseStatus}");
            
            foreach(var recipient in email.Recipients)
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
                    Status = emailResponseStatus,
                    Attempts = 1,
                };
                _emailHistoryInteractor.AddEmailHistory(emailObj);
            }
            //Update Quota Implemented Outside The Loop--revisit
            _emailQuotaInteractor.IncrementEmailQuota(email.ChannelKey);

        }

        /// <summary>
        /// Sends the email with attachments via the respective provider mentioned in provider settings and updates the history and quota tables.
        /// </summary>
        /// <param name="email"><see cref="Email"/> model</param>
        public void SendEmailWithAttachments(Email email)
        {
            _logger.LogInformation($"SendEmail method hit.");
            string sender = string.Empty;
            string message = email.GetMessage(_emailTemplateInteractor);
            _logger.LogInformation($"Message: {message}");
            // If external application didnot send the sender value, get it from template.
            if (string.IsNullOrEmpty(email.Sender))
                sender = email.GetSender(_emailTemplateInteractor);
            else
                sender = email.Sender;

            if (string.IsNullOrEmpty(sender))
                sender = _emailProviderConnectionString.Fields["Sender"];
            _logger.LogInformation($"Sender: {sender}");
            string emailResponseStatus = _emailProvider.SendEmailWithAttachments(email.Recipients,email.Files, message, email.Subject, sender).Result;
            _logger.LogDebug($"Email response status: {emailResponseStatus}");

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
                    Status = emailResponseStatus,
                    Attempts = 1,
                };
                _emailHistoryInteractor.AddEmailHistory(emailObj);
            }
            //Update Quota Implemented Outside The Loop--revisit
            _emailQuotaInteractor.UpdateEmailQuota(email.ChannelKey);

        }
    }
}
