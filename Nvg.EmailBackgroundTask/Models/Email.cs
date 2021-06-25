using Microsoft.AspNetCore.Http;
using Nvg.EmailService;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Text;
using static Nvg.EmailBackgroundTask.Events.SendEmailEvent;


namespace Nvg.EmailBackgroundTask.Models
{
    public class Email
    {
        public Email()
        {
            MessageParts = new Dictionary<string, string>();
        }
        public List<string> Recipients { get; set; }
        public string Sender { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Variant { get; internal set; }
        public string ChannelKey { get; internal set; }
        public string ProviderName { get; internal set; }
        public string Tag { get; set; }
        public List<EmailAttachment> Files { get; set; }
        public string GetMessage(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, ChannelKey, Variant);
            var msg = template?.MessageTemplate;
            foreach (var item in MessageParts)
                msg = msg.Replace($"{{{item.Key}}}", item.Value);

            return msg;
        }

        public string GetSender(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, ChannelKey, Variant);
            return template?.Sender;
        }

    }
}
