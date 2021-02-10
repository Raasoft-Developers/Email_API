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

        public EmailManager(IEmailProvider emailProvider, IEmailTemplateInteractor emailTemplateInteractor, IEmailHistoryInteractor emailHistoryInteractor,
            IEmailQuotaInteractor emailQuotaInteractor, EmailProviderConnectionString emailProviderConnectionString)
        {
            _emailProvider = emailProvider;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _emailQuotaInteractor = emailQuotaInteractor;
            _emailProviderConnectionString = emailProviderConnectionString;
        }
        public void SendEmail(Email email)
        {
            string sender = string.Empty;
            string message = email.GetMessage(_emailTemplateInteractor);
            // If external application didnot send the sender value, get it from template.
            if (string.IsNullOrEmpty(email.Sender))
                sender = email.GetSender(_emailTemplateInteractor);
            else
                sender = email.Sender;

            if (string.IsNullOrEmpty(sender))
                sender = _emailProviderConnectionString.Fields["Sender"];

            string emailResponseStatus = _emailProvider.SendEmail(email.Recipients, message, email.Subject, sender).Result;

            var emailObj = new EmailHistoryDto()
            {
                MessageSent = message,
                Sender = sender,
                Recipients = email.Recipients,
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

            _emailQuotaInteractor.UpdateEmailQuota(email.ChannelKey);
        }
    }
}
