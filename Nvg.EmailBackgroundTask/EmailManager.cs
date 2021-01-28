using Nvg.EmailBackgroundTask.EmailProvider;
using Nvg.EmailBackgroundTask.Models;
using Nvg.EmailService;
using Nvg.EmailService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailBackgroundTask
{
    public class EmailManager
    {
        private readonly IEmailProvider _emailProvider;
        private readonly IEmailTemplateInteractor _emailTemplateInteractor;
        private readonly IEmailHistoryInteractor _emailHistoryInteractor;
        private readonly IEmailCounterInteractor _emailCounterInteractor;

        public EmailManager(IEmailProvider emailProvider, IEmailTemplateInteractor emailTemplateInteractor, IEmailHistoryInteractor emailHistoryInteractor, IEmailCounterInteractor emailCountInteractor)
        {
            _emailProvider = emailProvider;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _emailCounterInteractor = emailCountInteractor;
        }
        public void SendEmail(Email email)
        {
            string emailBody = email.GetEmail(_emailTemplateInteractor);
            string subject = email.GetSubject(_emailTemplateInteractor);
            _emailProvider.SendEmail(email.To, emailBody, subject, email.HtmlContent, email.Sender);

            var emailObj = new EmailHistoryDto()
            {
                CreatedOn = DateTime.UtcNow,
                MailBody = emailBody,
                ToEmailID = email.To,
                TenantID = email.TenantID,
                FacilityID = email.FacilityID,
                SentOn = DateTime.UtcNow,
                Status = "SENT"
            };
            _emailHistoryInteractor.Add(emailObj);

            _emailCounterInteractor.UpdateEmailCounter(email.TenantID, email.FacilityID);
        }
    }
}
