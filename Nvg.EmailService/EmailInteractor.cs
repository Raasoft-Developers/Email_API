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
            var sendEmailEvent = new SendEmailEvent();
            sendEmailEvent.TemplateName = "FORGOT_PASSWORD_NOTIFICATION";
            sendEmailEvent.Recipients = emailInputs.To;
            sendEmailEvent.TenantID = emailInputs.TenantID;
            sendEmailEvent.EmailParts = new Dictionary<string, string> {
                    { "Recipient", emailInputs.To},
                    { "PasswordResetLink", emailInputs.ResetPasswordLink}
                };
            sendEmailEvent.SubjectParts = new Dictionary<string, string> {
                    { "Recipient", emailInputs.To}
                };
            _eventBus.Publish(sendEmailEvent);
        }
    }
}
