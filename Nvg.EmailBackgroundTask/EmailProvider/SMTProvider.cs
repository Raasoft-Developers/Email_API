using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Nvg.EmailBackgroundTask.EmailProvider
{
    public class SMTPProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;
        private readonly ILogger<SMTPProvider> _logger;
        public SMTPProvider(EmailProviderConnectionString emailProviderConnectionString, ILogger<SMTPProvider> logger)
        {
            _emailProviderCS = emailProviderConnectionString;
            _logger = logger;
        }

        public async Task<string> SendEmail(string recipients, string message, string subject, string sender = null)
        {
            _logger.LogInformation("SendEmail method.");
            if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                sender = _emailProviderCS.Fields["Sender"];

            _logger.LogInformation("Sender: " + sender);
            /*
            // Gmail SMTP implementation
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(sender, sender));
            emailMessage.To.Add(new MailboxAddress(recipients, recipients));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = string.Format("{0} : {1}", message, htmlContent) };
            */

            MailMessage emailMessage = new MailMessage();
            emailMessage.To.Add(new MailAddress(recipients));
            emailMessage.From = new MailAddress(sender);
            emailMessage.Subject = subject;
            emailMessage.Body = message;
            emailMessage.IsBodyHtml = true;

            await SendAsync(emailMessage, sender);
            return "Success";
        }
        private async Task SendAsync(/*MimeMessage*/ MailMessage mailMessage, string sender)
        {
            var smtpServer = _emailProviderCS.Fields["SmtpServer"];
            var password = _emailProviderCS.Fields["Password"];
            var port = _emailProviderCS.Fields["Port"];
            _logger.LogInformation("smtpServer: " + smtpServer);
            _logger.LogInformation("password: " + password);
            _logger.LogInformation("port: " + port);
            // Office365/Outlook SMTP implementation.
            int portNo = 587;
            if (!string.IsNullOrEmpty(port))
                portNo = Convert.ToInt32(port);

            using (var client = new System.Net.Mail.SmtpClient())
            {
                try
                {
                    _logger.LogInformation("Initializing SMTP Client");
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(sender, password);
                    client.Port = portNo; // You can use Port 25 if 587 is blocked
                    client.Host = smtpServer;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    _logger.LogInformation("Sending Email");
                    client.Send(mailMessage);
                    _logger.LogInformation("Email Sent");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred when sending email: " +ex.Message);
                    throw ex;
                }
                finally
                {
                    _logger.LogInformation("Disposing SMTP Client");
                    client.Dispose();
                }
            }

            /*
            // Gmail SMTP implementation
            int portNo = 465;
            if (!string.IsNullOrEmpty(port))
                portNo = Convert.ToInt32(port);

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpServer, portNo, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(sender, password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            */
        }
    }
}
