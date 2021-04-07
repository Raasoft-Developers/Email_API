using Microsoft.Extensions.Logging;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailChannel;
using Nvg.EmailService.EmailHistory;
using Nvg.EmailService.EmailPool;
using Nvg.EmailService.EmailProvider;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailInteractor : IEmailInteractor
    {
        private readonly IEmailEventInteractor _emailEventInteractor;
        private readonly IEmailPoolInteractor _emailPoolInteractor;
        private readonly IEmailProviderInteractor _emailProviderInteractor;
        private readonly IEmailChannelInteractor _emailChannelInteractor;
        private readonly IEmailTemplateInteractor _emailTemplateInteractor;
        private readonly IEmailHistoryInteractor _emailHistoryInteractor;
        private readonly ILogger<EmailInteractor> _logger;

        public EmailInteractor(IEmailEventInteractor emailEventInteractor,
            IEmailPoolInteractor emailPoolInteractor, IEmailProviderInteractor emailProviderInteractor,
            IEmailChannelInteractor emailChannelInteractor, IEmailTemplateInteractor emailTemplateInteractor,
            IEmailHistoryInteractor emailHistoryInteractor, ILogger<EmailInteractor> logger)
        {
            _emailEventInteractor = emailEventInteractor;
            _emailPoolInteractor = emailPoolInteractor;
            _emailProviderInteractor = emailProviderInteractor;
            _emailChannelInteractor = emailChannelInteractor;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
            _logger = logger;
        }

        public EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto poolInput)
        {
            _logger.LogInformation("AddEmailPool interactor method.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailPool.");
                poolResponse = _emailPoolInteractor.AddEmailPool(poolInput);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while adding email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddEmailProvider interactor method.");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailProvider.");
                providerResponse = _emailProviderInteractor.AddEmailProvider(providerInput);
                _logger.LogDebug("Status: "+ providerResponse.Status+", " + providerResponse.Message);
                return providerResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while adding email provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<EmailProviderSettingsDto> UpdateEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateEmailProvider interactor method.");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                _logger.LogInformation("Trying to update EmailProvider.");
                providerResponse = _emailProviderInteractor.UpdateEmailProvider(providerInput);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while updating email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while updating email provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("AddEmailChannel interactor method.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailChannel.");
                channelResponse = _emailChannelInteractor.AddEmailChannel(channelInput);
                _logger.LogDebug("" + channelResponse.Message);
                return channelResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while adding email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> UpdateEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("UpdateEmailChannel interactor method.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                _logger.LogInformation("Trying to update EmailChannel.");
                channelResponse = _emailChannelInteractor.UpdateEmailChannel(channelInput);
                _logger.LogDebug("" + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while updating email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while updating email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("AddEmailTemplate interactor method.");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailTemplate.");
                templateResponse = _emailTemplateInteractor.AddEmailTemplate(templateInput);
                _logger.LogDebug("" + templateResponse.Message);
                return templateResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email template: ", ex.Message);
                templateResponse.Message = "Error occurred while adding email template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public EmailResponseDto<EmailTemplateDto> UpdateEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateEmailTemplate interactor method.");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to update EmailTemplate.");
                templateResponse = _emailTemplateInteractor.UpdateEmailTemplate(templateInput);
                _logger.LogDebug("" + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while updating email template: ", ex.Message);
                templateResponse.Message = "Error occurred while updating email template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey)
        {
            _logger.LogInformation("GetEmailChannelByKey interactor method.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                _logger.LogInformation($"Trying to get Email Channel By Key for Channel Key {channelKey}.");
                channelResponse = _emailChannelInteractor.GetEmailChannelByKey(channelKey);
                _logger.LogDebug("" + channelResponse.Message);
                return channelResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while getting email channel by key: ", ex.Message);
                channelResponse.Message = "Error occurred while getting email channel by key: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("GetEmailProvidersByPool interactor method.");
            EmailResponseDto<List<EmailProviderSettingsDto>> providerResponse = new EmailResponseDto<List<EmailProviderSettingsDto>>();
            try
            {
                _logger.LogInformation($"Trying to get Email Provider By poolname for Pool: {poolName} and Provider: {providerName}.");
                providerResponse = _emailProviderInteractor.GetEmailProvidersByPool(poolName, providerName);
                _logger.LogDebug("" + providerResponse.Message);
                return providerResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while getting email provider by pool: ", ex.Message);
                providerResponse.Message = "Error occurred while getting email provider by pool: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("GetEmailHistoriesByTag interactor method.");
            EmailResponseDto<List<EmailHistoryDto>> poolResponse = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                _logger.LogInformation($"Trying to get Email histories by tag for ChannelKey: {channelKey} and Tag: {tag}.");
                poolResponse = _emailHistoryInteractor.GetEmailHistoriesByTag(channelKey, tag);
                _logger.LogDebug("" + poolResponse.Message);
                return poolResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while getting email histories: ", ex.Message);
                poolResponse.Message = "Error occurred while getting email histories: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<string> SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation("SendMail interactor method.");
            var response = new EmailResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(emailInputs.ChannelKey))
                {
                    _logger.LogError("Channel key cannot be blank.");
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _emailChannelInteractor.CheckIfChannelExist(emailInputs.ChannelKey).Result;
                    if (!channelExist)
                    {
                        _logger.LogError($"Invalid Channel key {emailInputs.ChannelKey}.");
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(emailInputs.TemplateName))
                {
                    _logger.LogError($"Template name cannot be blank.");
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _emailTemplateInteractor.CheckIfTemplateExist(emailInputs.ChannelKey, emailInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        _logger.LogError($"No template found for template name {emailInputs.TemplateName} and channel key {emailInputs.ChannelKey}.");
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {emailInputs.TemplateName} and channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                _logger.LogInformation("Trying to send Email.");
                _emailEventInteractor.SendMail(emailInputs);
                response.Status = true;
                response.Message = $"Email is sent successfully to {emailInputs.Recipients}.";
                _logger.LogDebug(""+response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while sending email: ", ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
