using Nvg.EmailService;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.Api.Email.Models
{
    public class Email
    {
        public Email()
        {
            MessageParts = new Dictionary<string, string>();
        }
        public string Recipients { get; set; }
        public string Sender { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Variant { get; internal set; }
        public string ChannelKey { get; internal set; }
        public string ProviderName { get; internal set; }
        public string Tag { get; set; }


        public string GetMessage(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, ChannelKey, Variant);
            var msg = template?.MessageTemplate;
            foreach (var item in MessageParts)
                msg = msg.ToLower().Replace($"{{{item.Key.ToLower()}}}", item.Value);
            return msg;
        }

        public string GetSender(IEmailTemplateInteractor emailTemplateInteractor)
        {
            var template = emailTemplateInteractor.GetEmailTemplate(TemplateName, ChannelKey, Variant);
            return template?.Sender;
        }

    }
}
