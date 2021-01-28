using Nvg.EmailService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailBackgroundTask.Models
{
    public class Email
    {
        public Email()
        {
            EmailParts = new Dictionary<string, string>();
        }
        public string To { get; set; }
        public string Sender { get; set; }
        public Dictionary<string, string> Subject { get; set; }

        public string MailBody { get; set; }
        public string HtmlContent { get; set; }

        public string TemplateName { get; set; }
        public Dictionary<string, string> EmailParts { get; set; }
        public string TenantID { get; internal set; }
        public string FacilityID { get; internal set; }


        public string GetEmail(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, TenantID, FacilityID);

            var msg = template.EmailBodyTemplate;
            foreach (var item in EmailParts)
            {
                msg = msg.Replace($"{{{item.Key}}}", item.Value);
            }
            return msg;
        }
        public string GetSubject(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, TenantID, FacilityID);

            var subject = template.SubjectTemplate;
            foreach (var item in Subject)
            {
                subject = subject.Replace($"{{{item.Key}}}", item.Value);
            }
            return subject;
        }

    }
}
