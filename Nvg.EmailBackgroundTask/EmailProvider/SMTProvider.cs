using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Azure.Amqp.Framing;
using System.Net.Mail;

namespace Nvg.EmailBackgroundTask.EmailProvider
{
    public class SMTPProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;

        public SMTPProvider(EmailProviderConnectionString emailProviderConnectionString)
        {
            _emailProviderCS = emailProviderConnectionString;
        }

        public async Task<string> SendEmail(string recipients, string message, string subject, string htmlContent, string sender = "")
        {
            if (!string.IsNullOrEmpty(_emailProviderCS.Sender))
                sender = _emailProviderCS.Sender;
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
            emailMessage.Body = message + ", " + htmlContent;
            emailMessage.IsBodyHtml = true;

            await SendAsync(emailMessage, sender);
            return "Success";
        }
        private async Task SendAsync(/*MimeMessage*/ MailMessage mailMessage, string sender)
        {
            var smtpServer = _emailProviderCS.Fields["SmtpServer"];
            var password = _emailProviderCS.Fields["Password"];
            var port = _emailProviderCS.Fields["Port"];

            // Office365/Outlook SMTP implementation.
            int portNo = 587;
            if (!string.IsNullOrEmpty(port))
                portNo = Convert.ToInt32(port);

            using (var client = new System.Net.Mail.SmtpClient())
            {
                try
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(sender, password);
                    client.Port = portNo; // You can use Port 25 if 587 is blocked
                    client.Host = smtpServer;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
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
