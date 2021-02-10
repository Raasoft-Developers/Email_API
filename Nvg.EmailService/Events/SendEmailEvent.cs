using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Events
{
    public class SendEmailEvent : IntegrationEvent
    {
        public string ChannelKey { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Tag { get; set; }
    }
}
