using EventBus.Abstractions;
using Nvg.EmailService.Dtos;
using Nvg.EmailService.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService
{
    public class EmailInteractor : IEmailInteractor
    {
        private readonly IEventBus _eventBus;

        public EmailInteractor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void SendMail(EmailDto emailInputs)
        {
            string user = (!string.IsNullOrEmpty(emailInputs.Username) ? emailInputs.Username : emailInputs.To);
            var sendEmailEvent = new SendEmailEvent();
            sendEmailEvent.TemplateName = emailInputs.Template;
            sendEmailEvent.Recipients = emailInputs.To;
            sendEmailEvent.EmailParts = new Dictionary<string, string> {
                { "User", user},
                { "Content", emailInputs.Content}
            };
            sendEmailEvent.SubjectParts = new Dictionary<string, string> {
                { "User", user},
                { "Subject", emailInputs.Subject}
            };
            sendEmailEvent.TenantID = emailInputs.TenantID;
            sendEmailEvent.FacilityID = emailInputs.FacilityID;
            _eventBus.Publish(sendEmailEvent);
        }
    }
}
