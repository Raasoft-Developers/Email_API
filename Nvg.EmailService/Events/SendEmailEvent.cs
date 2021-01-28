using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Events
{
    public class SendEmailEvent : IntegrationEvent
    {
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Recipients { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> SubjectParts { get; set; }
        public Dictionary<string, string> EmailParts { get; set; }
    }
}
