using EmailService.Data.Entities;
using EmailService.DTOS;
using EmailService.EmailErrorLog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailService.EmailServiceProviders
{
    /// <summary>
    /// SMTP Email Provider.
    /// </summary>
    public class SMTPProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;
        private readonly ILogger<SMTPProvider> _logger;
        private readonly IEmailErrorLogInteractor _emailErrorLogInteractor;


        public SMTPProvider(EmailProviderConnectionString emailProviderConnectionString, ILogger<SMTPProvider> logger, IEmailErrorLogInteractor emailErrorLogInteractor)
        {
            _emailProviderCS = emailProviderConnectionString;
            _emailErrorLogInteractor = emailErrorLogInteractor;
            _logger = logger;
        }

        public async Task<string> SendEmail(string channelKey, List<string> recipients, string message, string subject, string sender = null)
        {
            try
            {
                //_logger.LogInformation("SendEmail method.");
                if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                    sender = _emailProviderCS.Fields["Sender"];

                //_logger.LogInformation("Sender: " + sender);
                /*
                // Gmail SMTP implementation
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(sender, sender));
                emailMessage.To.Add(new MailboxAddress(recipients, recipients));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = string.Format("{0} : {1}", message, htmlContent) };
                */

                MailMessage emailMessage = new MailMessage();
                foreach (var recipient in recipients)
                    emailMessage.To.Add(new MailAddress(recipient));
                emailMessage.From = new MailAddress(sender);
                emailMessage.Subject = subject;
                emailMessage.Body = message;
                emailMessage.IsBodyHtml = true;
                await SendAsync(emailMessage, sender);
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception : " + ex.Message);
                var emailErrorLog = new EmailErrorLogTable
                {
                    Description = ex.Message.ToString(),
                    ErrorType = ex.GetType().Name.ToString(),
                    Recipients = string.Join(",", recipients),
                    Subject = subject,
                    CreationTime = DateTime.UtcNow,
                    StackTrace = ex.StackTrace.ToString(),
                    ChannelKey = channelKey
                };
                _emailErrorLogInteractor.AddEmailErrorLog(emailErrorLog);
                return "Excpetion Occured. Failed to send email. Check Error Log Table for details";
                throw ex;
            }
        }
        public async Task<string> SendEmailWithAttachments(string channelKey, List<string> recipients, List<EmailAttachment> files, string message, string subject, string sender = null)
        {
            try
            {
                // _logger.LogInformation("SendEmail method.");
                if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                    sender = _emailProviderCS.Fields["Sender"];

                //_logger.LogInformation("Sender: " + sender);
                /*
                // Gmail SMTP implementation
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(sender, sender));
                emailMessage.To.Add(new MailboxAddress(recipients, recipients));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = string.Format("{0} : {1}", message, htmlContent) };
                */

                MailMessage emailMessage = new MailMessage();
                foreach (var recipient in recipients)
                    emailMessage.To.Add(new MailAddress(recipient));
                emailMessage.From = new MailAddress(sender);
                emailMessage.Subject = subject;
                emailMessage.Body = message;
                emailMessage.IsBodyHtml = true;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        try
                        {
                            MemoryStream ms = new MemoryStream(Convert.FromBase64String(file.FileContent));
                            Attachment attachment = new Attachment(ms, file.FileName, file.ContentType);
                            emailMessage.Attachments.Add(attachment);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception : " + ex.Message);
                            var emailErrorLog = new EmailErrorLogTable
                            {
                                Description = ex.Message.ToString(),
                                ErrorType = ex.GetType().Name.ToString(),
                                Recipients = string.Join(",", recipients),
                                Subject = subject,
                                CreationTime = DateTime.UtcNow,
                                StackTrace = ex.StackTrace.ToString(),
                                ChannelKey = channelKey
                            };
                            _emailErrorLogInteractor.AddEmailErrorLog(emailErrorLog);
                            return "Excpetion Occured while attaching files. Failed to send email. Check Error Log Table for details";
                            throw ex;
                        }
                    }
                }
                await SendAsync(emailMessage, sender);
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception : " + ex.Message);
                var emailErrorLog = new EmailErrorLogTable
                {
                    Description = ex.Message.ToString(),
                    ErrorType = ex.GetType().Name.ToString(),
                    Recipients = string.Join(",", recipients),
                    Subject = subject,
                    StackTrace = ex.StackTrace.ToString(),
                    CreationTime = DateTime.UtcNow,
                    ChannelKey = channelKey
                };
                _emailErrorLogInteractor.AddEmailErrorLog(emailErrorLog);
                return "Excpetion Occured. Failed to send email. Check Error Log Table for details";
                throw ex;
            }
        }
        private async Task SendAsync(/*MimeMessage*/ MailMessage mailMessage, string sender)
        {
            var smtpServer = _emailProviderCS.Fields["SmtpServer"];
            string password = string.Empty;

            if (_emailProviderCS.Fields.ContainsKey("EncodedPassword"))
            {
                password = Base64Decoder(_emailProviderCS.Fields["EncodedPassword"]);
            }
            else if (_emailProviderCS.Fields.ContainsKey("Password"))
            {
                password = _emailProviderCS.Fields["Password"];
            }

            var port = _emailProviderCS.Fields["Port"];
            _logger.LogInformation("smtpServer: " + smtpServer);
            _logger.LogInformation("sender: " + sender);
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
                    //_logger.LogInformation("Email Sent");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred when sending email: " + ex);
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

        /// <summary>
        /// Converts base 64 string to utf-8 string
        /// </summary>
        /// <param name="base64EncodedData">base 64 encoded string</param>
        /// <returns>utf-8 string</returns>
        private static string Base64Decoder(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
