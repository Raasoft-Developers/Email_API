using EventBus.Events;
using System.Collections.Generic;

namespace EmailBackgroundTask.Events
{
    public class SendEmailEvent : IntegrationEvent
    {
        public string ChannelKey { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Tag { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }

        public SendEmailEvent(string sender, List<string> recipients, string templateName)
        {
            Sender = sender;
            Recipients = recipients;
            TemplateName = templateName;
        }
    }
}
