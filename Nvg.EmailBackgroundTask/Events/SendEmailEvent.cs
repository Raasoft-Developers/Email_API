using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Events;

namespace Nvg.EmailBackgroundTask.Events
{
    public class SendEmailEvent : IntegrationEvent
    {
        public string ChannelKey { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string TemplateName { get; set; }
        public string Tag { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }

        public SendEmailEvent(string sender, string recipients, string templateName)
        {
            Sender = sender;
            Recipients = recipients;
            TemplateName = templateName;
        }
    }
}
