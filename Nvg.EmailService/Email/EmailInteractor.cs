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
            _logger.LogInformation("In EmailInteractor: AddEmailPool interactor method hit.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                poolResponse = _emailPoolInteractor.AddEmailPool(poolInput);
                _logger.LogDebug("In EmailInteractor: "+poolResponse.Message);
                return poolResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while adding email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while adding email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("In EmailInteractor: AddEmailProvider interactor method hit.");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                providerResponse = _emailProviderInteractor.AddEmailProvider(providerInput);
                _logger.LogDebug("In EmailInteractor: " + providerResponse.Message);
                return providerResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while adding email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while adding email provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("In EmailInteractor: AddEmailChannel interactor method hit.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                channelResponse = _emailChannelInteractor.AddEmailChannel(channelInput);
                _logger.LogDebug("In EmailInteractor: " + channelResponse.Message);
                return channelResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while adding email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while adding email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("In EmailInteractor: AddEmailTemplate interactor method hit.");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                templateResponse = _emailTemplateInteractor.AddEmailTemplate(templateInput);
                _logger.LogDebug("In EmailInteractor: " + templateResponse.Message);
                return templateResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while adding email template: ", ex.Message);
                templateResponse.Message = "Error occurred while adding email template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey)
        {
            _logger.LogInformation("In EmailInteractor: GetEmailChannelByKey interactor method hit.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                channelResponse = _emailChannelInteractor.GetEmailChannelByKey(channelKey);
                _logger.LogDebug("In EmailInteractor: " + channelResponse.Message);
                return channelResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while getting email channel by key: ", ex.Message);
                channelResponse.Message = "Error occurred while getting email channel by key: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("In EmailInteractor: GetEmailProvidersByPool interactor method hit.");
            EmailResponseDto<List<EmailProviderSettingsDto>> providerResponse = new EmailResponseDto<List<EmailProviderSettingsDto>>();
            try
            {
                providerResponse = _emailProviderInteractor.GetEmailProvidersByPool(poolName, providerName);
                _logger.LogDebug("In EmailInteractor: " + providerResponse.Message);
                return providerResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while getting email provider by pool: ", ex.Message);
                providerResponse.Message = "Error occurred while getting email provider by pool: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("In EmailInteractor: GetEmailHistoriesByTag interactor method hit.");
            EmailResponseDto<List<EmailHistoryDto>> poolResponse = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                poolResponse = _emailHistoryInteractor.GetEmailHistoriesByTag(channelKey, tag);
                _logger.LogDebug("In EmailInteractor: " + poolResponse.Message);
                return poolResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while getting email histories: ", ex.Message);
                poolResponse.Message = "Error occurred while getting email histories: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<string> SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation("In EmailInteractor: SendMail interactor method hit.");
            var response = new EmailResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(emailInputs.ChannelKey))
                {
                    _logger.LogError("In EmailInteractor: Channel key cannot be blank.");
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _emailChannelInteractor.CheckIfChannelExist(emailInputs.ChannelKey).Result;
                    if (!channelExist)
                    {
                        _logger.LogError($"In EmailInteractor: Invalid Channel key {emailInputs.ChannelKey}.");
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(emailInputs.TemplateName))
                {
                    _logger.LogError($"In EmailInteractor: Template name cannot be blank.");
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _emailTemplateInteractor.CheckIfTemplateExist(emailInputs.ChannelKey, emailInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        _logger.LogError($"In EmailInteractor: No template found for template name {emailInputs.TemplateName} and channel key {emailInputs.ChannelKey}.");
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {emailInputs.TemplateName} and channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                _emailEventInteractor.SendMail(emailInputs);
                response.Status = true;
                response.Message = $"Email is sent successfully to {emailInputs.Recipients}.";
                _logger.LogDebug("In EmailInteractor: "+response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("In EmailInteractor: Error occurred in Email Interactor while sending email: ", ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
