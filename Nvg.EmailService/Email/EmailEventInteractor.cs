using EventBus.Abstractions;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailEventInteractor : IEmailEventInteractor
    {
        private readonly IEventBus _eventBus;

        public EmailEventInteractor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void SendMail(EmailDto emailInputs)
        {
            string user = (!string.IsNullOrEmpty(emailInputs.Username) ? emailInputs.Username : emailInputs.Recipients);
            var sendEmailEvent = new SendEmailEvent();
            sendEmailEvent.ChannelKey = emailInputs.ChannelKey;
            sendEmailEvent.TemplateName = emailInputs.TemplateName;
            sendEmailEvent.Variant = emailInputs.Variant;
            sendEmailEvent.Sender = emailInputs.Sender;
            sendEmailEvent.Recipients = emailInputs.Recipients;
            sendEmailEvent.Subject = emailInputs.Subject;
            sendEmailEvent.MessageParts = new Dictionary<string, string> {
                { "User", user},
                { "Body", emailInputs.Body}
            };
            sendEmailEvent.Tag = emailInputs.Tag;
            _eventBus.Publish(sendEmailEvent);
        }
    }
}
