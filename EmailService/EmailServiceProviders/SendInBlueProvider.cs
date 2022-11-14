using EmailService.Data.Entities;
using EmailService.DTOS;
using EmailService.EmailErrorLog;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailService.EmailServiceProviders
{
    /// <summary>
    /// Send Grid Email Provider.
    /// </summary>
    public class SendInBlueProvider : IEmailProvider
    {
        private readonly EmailProviderConnectionString _emailProviderCS;
        private readonly ILogger<SendInBlueProvider> _logger;
        private readonly IEmailErrorLogInteractor _emailErrorLogInteractor;


        public SendInBlueProvider(EmailProviderConnectionString emailProviderConnectionString, ILogger<SendInBlueProvider> logger, IEmailErrorLogInteractor emailErrorLogInteractor)
        {
            _emailProviderCS = emailProviderConnectionString;
            _emailErrorLogInteractor = emailErrorLogInteractor;
            _logger = logger;
        }

        public async Task<string> SendEmail(string channelKey, List<string> recipients, string message, string subject, string sender = "")
        {
            try
            {
                // _logger.LogInformation("SendEmail method.");
                if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                    sender = _emailProviderCS.Fields["Sender"];
                var senderName = _emailProviderCS.Fields["SenderName"];

                // _logger.LogInformation("Sender: " + sender);
                var APIKey = _emailProviderCS.Fields["ApiKey"];
                if (Configuration.Default.ApiKey.ContainsKey("api-key"))
                {
                    string currentKey;
                    Configuration.Default.ApiKey.TryGetValue("api-key", out currentKey);
                    if (currentKey != APIKey)
                    {
                        Configuration.Default.ApiKey.Remove("api-key");
                        Configuration.Default.ApiKey.Add("api-key", APIKey);
                    }
                }
                else
                {
                    Configuration.Default.ApiKey.Add("api-key", APIKey);
                }
                var apiInstance = new TransactionalEmailsApi();
                SendSmtpEmailSender from = new SendSmtpEmailSender(senderName, sender);
                List<SendSmtpEmailTo> to = new List<SendSmtpEmailTo>();
                foreach (var recipient in recipients)
                {
                    SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(recipient);
                    to.Add(smtpEmailTo);
                }
                _logger.LogInformation("client: " + apiInstance);
                _logger.LogInformation("from: " + from);
                _logger.LogInformation("to: " + to);
                _logger.LogInformation("Sending Email...");

                var sendSmtpEmail = new SendSmtpEmail(sender: from, to: to, null, null, htmlContent: message, null, subject: subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                _logger.LogInformation(result.ToJson());
                var emailRes = ProcessResponse(result.ToJson());
                _logger.LogInformation("Response: " + emailRes.Message);
                return emailRes.Message;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
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

        public async Task<string> SendEmailWithAttachments(string channelKey, List<string> recipients, List<EmailAttachment> files, string message, string subject, string sender = "")
        {

            try
            {
                // _logger.LogInformation("SendEmail method.");
                if (!string.IsNullOrEmpty(_emailProviderCS.Fields["Sender"]))
                    sender = _emailProviderCS.Fields["Sender"];
                var senderName = _emailProviderCS.Fields["SenderName"];
                // _logger.LogInformation("Sender: " + sender);
                var APIKey = _emailProviderCS.Fields["ApiKey"];
                Configuration.Default.ApiKey.Add("api-key", APIKey);
                var apiInstance = new TransactionalEmailsApi();
                SendSmtpEmailSender from = new SendSmtpEmailSender(senderName, sender);
                List<SendSmtpEmailTo> to = new List<SendSmtpEmailTo>();
                foreach (var recipient in recipients)
                {
                    SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(recipient);
                    to.Add(smtpEmailTo);
                }
                _logger.LogInformation("client: " + apiInstance);
                _logger.LogInformation("from: " + from);
                _logger.LogInformation("to: " + to);


                List<SendSmtpEmailAttachment> attachments = new List<SendSmtpEmailAttachment>();
                try
                {
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            byte[] content = System.Convert.FromBase64String(file.FileContent);
                            SendSmtpEmailAttachment attachmentContent = new SendSmtpEmailAttachment(null, content, file.FileName);
                            attachments.Add(attachmentContent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception occured while attaching files : " + ex.Message);
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

                _logger.LogInformation("Sending Email...");


                var sendSmtpEmail = new SendSmtpEmail(sender: from, to: to, null, null, htmlContent: message, null, subject: subject, attachment: attachments);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                _logger.LogInformation(result.ToJson());
                var emailRes = ProcessResponse(result.ToJson());
                _logger.LogInformation("Response: " + emailRes.Message);
                return emailRes.Message;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
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
        private EmailResponseDto<string> ProcessResponse(string response)
        {
            var emailResponse = new EmailResponseDto<string>();
            if (!string.IsNullOrEmpty(response))
            {
                JObject json = JObject.Parse(response);
                var error = json["message"];
                if (error == null)
                {
                    emailResponse.Status = true;
                    emailResponse.Message = "Email has been successfully sent.";
                    return emailResponse;
                }
                else
                {
                    emailResponse.Message = error.ToString();
                    emailResponse.Status = false;
                }
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
