using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmailEventInteractor> _logger;

        public EmailEventInteractor(IEventBus eventBus, ILogger<EmailEventInteractor> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public void SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation($"SendMail method hit");
            _logger.LogInformation($"Channel Key: {emailInputs.ChannelKey} , Template Name: {emailInputs.TemplateName}, Variant: {emailInputs.Variant}, Recipients: {emailInputs.Recipients}, Sender: {emailInputs.Sender}.");
            //string user = (!string.IsNullOrEmpty(emailInputs.Username) ? emailInputs.Username : emailInputs.Recipients);
            var sendEmailEvent = new SendEmailEvent();
            sendEmailEvent.ChannelKey = emailInputs.ChannelKey;
            sendEmailEvent.TemplateName = emailInputs.TemplateName;
            sendEmailEvent.Variant = emailInputs.Variant;
            sendEmailEvent.Sender = emailInputs.Sender;
            sendEmailEvent.Recipients = emailInputs.Recipients;
            sendEmailEvent.Subject = emailInputs.Subject;
            sendEmailEvent.MessageParts = emailInputs.MessageParts;
            sendEmailEvent.Tag = emailInputs.Tag;
            _logger.LogInformation("Publishing Email data.");
            _eventBus.Publish(sendEmailEvent);            
        }
    }
}
