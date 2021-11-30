﻿using Microsoft.Extensions.Logging;
using Nvg.EmailService.DTOS;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nvg.EmailService.EmailServiceProviders
{
    /// <summary>
    /// Send Grid Email Provider.
    /// </summary>
    public class SendGridProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;
        private readonly ILogger<SendGridProvider> _logger;

        public SendGridProvider(EmailProviderConnectionString emailProviderConnectionString, ILogger<SendGridProvider> logger)
        {
            _emailProviderCS = emailProviderConnectionString;
            _logger = logger;
        }

        public async Task<string> SendEmail(List<string> recipients, string message, string subject, string sender = "")
        {
            // _logger.LogInformation("SendEmail method.");
            if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                sender = _emailProviderCS.Fields["Sender"];
            // _logger.LogInformation("Sender: " + sender);
            var APIKey = _emailProviderCS.Fields["ApiKey"];
            var client = new SendGridClient(APIKey);
            var from = new EmailAddress(sender);
            var to = new List<EmailAddress>();
            foreach (var recipient in recipients)
                 to.Add(new EmailAddress(recipient));
            bool showAllRecipients = true;
            var email = MailHelper.CreateSingleEmailToMultipleRecipients(
                from,
                to,
                subject,
                null,
                message,
                showAllRecipients
            );
            _logger.LogInformation("client: " + client);
            _logger.LogInformation("from: " + from);
            _logger.LogInformation("to: " + to);
           // _logger.LogInformation("ApiKey: " + APIKey);
            _logger.LogInformation("Sending Email...");
            email.SetClickTracking(enable: false, enableText: false);

            var apiResponse = await client.SendEmailAsync(email);
            var emailRes = ProcessResponse(apiResponse);
            _logger.LogInformation("Response: " + emailRes.Message);
            return emailRes.Message;

        }
       
        public async Task<string> SendEmailWithAttachments(List<string> recipients, List<EmailAttachment> files, string message, string subject, string sender = "")
        {
            // _logger.LogInformation("SendEmail method.");
            if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                sender = _emailProviderCS.Fields["Sender"];
            // _logger.LogInformation("Sender: " + sender);
            var APIKey = _emailProviderCS.Fields["ApiKey"];
            var client = new SendGridClient(APIKey);
            var from = new EmailAddress(sender);
            var to = new List<EmailAddress>();
            foreach (var recipient in recipients)
                 to.Add(new EmailAddress(recipient));
            bool showAllRecipients = true;
            var email = MailHelper.CreateSingleEmailToMultipleRecipients(
                from,
                to,
                subject,
                null,
                message,
                showAllRecipients
            );
            try
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var attachment = new Attachment()
                        {
                            Content = file.FileContent,
                            Type = file.ContentType,
                            Filename = file.FileName,
                            Disposition = "attachment",
                            ContentId = file.FileName
                        };
                        email.AddAttachment(attachment);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed to Attach Image: " + ex.Message);
                throw ex;
            }
            _logger.LogInformation("client: " + client);
            _logger.LogInformation("from: " + from);
            _logger.LogInformation("to: " + to);
            _logger.LogInformation("ApiKey: " + APIKey);
            _logger.LogInformation("Sending Email...");
            email.SetClickTracking(enable: false, enableText: false);

            var apiResponse = await client.SendEmailAsync(email);
            var emailRes = ProcessResponse(apiResponse);
            _logger.LogInformation("Response: " + emailRes.Message);
            return emailRes.Message;
        }
        private EmailResponseDto<string> ProcessResponse(Response response)
        {
            var emailResponse = new EmailResponseDto<string>();
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted)
                || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                emailResponse.Status = true;
                emailResponse.Message = "Email has been successfully sent.";
                return emailResponse;
            }
            string errorResponse = response.Body.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(errorResponse))
            {
                JObject json = JObject.Parse(errorResponse);
                var errors = json["errors"].Children().ToList();
                var errorMsgs = new List<string>();
                foreach (var error in errors)
                {
                    var message = error["message"].ToString();
                    errorMsgs.Add(message);
                }
                emailResponse.Message = string.Join(".", errorMsgs);
                emailResponse.Status = false;
            }
            else
            {
                emailResponse.Status = false;
                emailResponse.Message = "Failed to send Email.";
            }
            return emailResponse;
        }
    }
}
