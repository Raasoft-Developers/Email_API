using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Nvg.EmailBackgroundTask.EmailProvider
{
    public class SendGridProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;

        public SendGridProvider(EmailProviderConnectionString emailProviderConnectionString)
        {
            _emailProviderCS = emailProviderConnectionString;
        }

        public async Task<string> SendEmail(string recipients, string message, string subject, string htmlContent, string sender = "")
        {
            if (!string.IsNullOrEmpty(_emailProviderCS.Sender))
                sender = _emailProviderCS.Sender;
            var APIKey = _emailProviderCS.Fields["ApiKey"];
            var client = new SendGridClient(APIKey);
            var from = new EmailAddress(sender);
            var to = new EmailAddress(recipients);
            var email = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                message,
                htmlContent
            );
            var apiResponse = await client.SendEmailAsync(email);
            return apiResponse.ToString();
        }
    }
}
