using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Nvg.EmailBackgroundTask.EmailProvider
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
            _logger.LogInformation("SendEmail method.");
            if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                sender = _emailProviderCS.Fields["Sender"];
            _logger.LogInformation("Sender: " + sender);
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
            _logger.LogInformation("ApiKey: " + APIKey);
            _logger.LogInformation("Sending Email...");
            var apiResponse = await client.SendEmailAsync(email);
            _logger.LogInformation("Response: " + apiResponse.ToString());
            return apiResponse.ToString();
        }
    }
}
