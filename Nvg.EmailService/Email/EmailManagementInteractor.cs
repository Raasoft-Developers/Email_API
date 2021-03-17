using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailHistory;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.EmailTemplate;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailManagementInteractor : IEmailManagementInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailPoolRepository _emailPoolRepository;
        private readonly IEmailProviderRepository _emailProviderRepository;
        private readonly IEmailChannelRepository _emailChannelRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailProviderInteractor _emailProviderInteractor;
        private readonly IEmailHistoryRepository _emailHistoryRepository;
        private readonly ILogger<EmailManagementInteractor> _logger;

        public EmailManagementInteractor(IMapper mapper, IEmailPoolRepository emailPoolRepository, IEmailProviderRepository emailProviderRepository,
            IEmailChannelRepository emailChannelRepository, IEmailTemplateRepository emailTemplateRepository, IEmailProviderInteractor emailProviderInteractor,
            IEmailHistoryRepository emailHistoryRepository, ILogger<EmailManagementInteractor> logger)
        {
            _mapper = mapper;
            _emailPoolRepository = emailPoolRepository;
            _emailProviderRepository = emailProviderRepository;
            _emailChannelRepository = emailChannelRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _emailProviderInteractor = emailProviderInteractor;
            _emailHistoryRepository = emailHistoryRepository;
            _logger = logger;
        }
        #region Email Pool
        public EmailResponseDto<List<EmailPoolDto>> GetEmailPools()
        {
            _logger.LogInformation("GetEmailPools interactor method.");
            EmailResponseDto<List<EmailPoolDto>> poolResponse = new EmailResponseDto<List<EmailPoolDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Pools.");
                var response = _emailPoolRepository.GetEmailPools();
                poolResponse = _mapper.Map<EmailResponseDto<List<EmailPoolDto>>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch(Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while getting email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<List<EmailPoolDto>> GetEmailPoolNames()
        {
            _logger.LogInformation("GetEmailPoolNames interactor method.");
            EmailResponseDto<List<EmailPoolDto>> poolResponse = new EmailResponseDto<List<EmailPoolDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Pool Names.");
                var response = _emailPoolRepository.GetEmailPoolNames();
                poolResponse = _mapper.Map<EmailResponseDto<List<EmailPoolDto>>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email pool names: ", ex.Message);
                poolResponse.Message = "Error occurred while getting email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<EmailPoolDto> UpdateEmailPool(EmailPoolDto emailPoolInput)
        {
            _logger.LogInformation("UpdateEmailPool interactor method.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                var mappedEmailInput = _mapper.Map<EmailPoolTable>(emailPoolInput);
                _logger.LogInformation("Trying to update Email Pools.");
                var response = _emailPoolRepository.UpdateEmailPool(mappedEmailInput);
                poolResponse = _mapper.Map<EmailResponseDto<EmailPoolDto>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while updating email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while updating email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public EmailResponseDto<string> DeleteEmailPool(string poolID)
        {
            _logger.LogInformation("DeleteEmailPool interactor method.");
            _logger.LogDebug("Pool ID:" + poolID);
            EmailResponseDto<string> poolResponse = new EmailResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete Email Pools.");
                poolResponse = _emailPoolRepository.DeleteEmailPool(poolID);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while deleting email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while deleting email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }
        #endregion

        #region Email Provider
        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProviders(string poolID)
        {
            _logger.LogInformation("GetEmailProviders interactor method.");
            _logger.LogDebug("Pool ID:" + poolID);
            EmailResponseDto<List<EmailProviderSettingsDto>> providerResponse = new EmailResponseDto<List<EmailProviderSettingsDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Providers.");
                var response = _emailProviderRepository.GetEmailProviders(poolID);
                providerResponse = _mapper.Map<EmailResponseDto<List<EmailProviderSettingsDto>>>(response);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while getting email provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProviderNames(string poolID)
        {
            _logger.LogInformation("GetEmailProviderNames interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            EmailResponseDto<List<EmailProviderSettingsDto>> providerResponse = new EmailResponseDto<List<EmailProviderSettingsDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Providers.");
                var responseDto = _emailProviderRepository.GetEmailProviderNames(poolID);
                providerResponse = _mapper.Map<EmailResponseDto<List<EmailProviderSettingsDto>>>(responseDto);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email provider names: ", ex.Message);
                providerResponse.Message = "Error occurred while getting email provider names: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public EmailResponseDto<EmailProviderSettingsDto> AddUpdateEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddUpdateEmailProvider interactor method.");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailProvider.");
                var mappedEmailInput = _mapper.Map<EmailProviderSettingsTable>(providerInput);
                var response = _emailProviderRepository.AddUpdateEmailProvider(mappedEmailInput);
                providerResponse = _mapper.Map<EmailResponseDto<EmailProviderSettingsDto>>(response);
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

        public EmailResponseDto<string> DeleteEmailProvider(string providerID)
        {
            _logger.LogInformation("DeleteEmailProvider interactor method.");
            _logger.LogDebug("Provider ID:" + providerID);
            EmailResponseDto<string> providerResponse = new EmailResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete Email Provider.");
                providerResponse = _emailProviderRepository.DeleteEmailProvider(providerID);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while deleting email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while deleting email provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }
        #endregion

        #region Email Channel
        public EmailResponseDto<List<EmailChannelDto>> GetEmailChannelsByPool(string poolID)
        {
            _logger.LogInformation("GetEmailChannels interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            EmailResponseDto<List<EmailChannelDto>> channelResponse = new EmailResponseDto<List<EmailChannelDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Channels.");
                var response = _emailChannelRepository.GetEmailChannels(poolID);
                channelResponse = _mapper.Map<EmailResponseDto<List<EmailChannelDto>>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while getting email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> AddUpdateEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("UpdateEmailProvider interactor method.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailProvider.");
                var mappedEmailInput = _mapper.Map<EmailChannelTable>(channelInput);
                var response = _emailChannelRepository.AddUpdateEmailChannel(mappedEmailInput);
                channelResponse = _mapper.Map<EmailResponseDto<EmailChannelDto>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
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

        public EmailResponseDto<string> DeleteEmailChannel(string channelID)
        {
            _logger.LogInformation("DeleteEmailChannel interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            EmailResponseDto<string> channelResponse = new EmailResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete Email Channel.");
                channelResponse = _emailChannelRepository.DeleteEmailChannel(channelID);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while deleting email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while deleting email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<List<EmailChannelDto>> GetEmailChannelKeys()
        {
            _logger.LogInformation("GetEmailChannelKeys interactor method.");
            EmailResponseDto<List<EmailChannelDto>> channelResponse = new EmailResponseDto<List<EmailChannelDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Channel keys.");
                var response = _emailChannelRepository.GetEmailChannelKeys();
                channelResponse = _mapper.Map<EmailResponseDto<List<EmailChannelDto>>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email channel keys: ", ex.Message);
                channelResponse.Message = "Error occurred while getting email channel keys: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }
        #endregion

        #region Email Template
        public EmailResponseDto<List<EmailTemplateDto>> GetEmailTemplatesByPool(string poolID)
        {
            _logger.LogInformation("GetEmailTemplatesByPool interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            EmailResponseDto<List<EmailTemplateDto>> templateResponse = new EmailResponseDto<List<EmailTemplateDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Templates.");
                var response = _emailTemplateRepository.GetEmailTemplatesByPool(poolID);
                templateResponse = _mapper.Map<EmailResponseDto<List<EmailTemplateDto>>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email templates: ", ex.Message);
                templateResponse.Message = "Error occurred while getting email templates: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }
        public EmailResponseDto<EmailTemplateDto> AddUpdateEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateEmailProvider interactor method.");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailProvider.");
                var mappedEmailInput = _mapper.Map<EmailTemplateTable>(templateInput);
                var response = _emailTemplateRepository.AddUpdateEmailTemplate(mappedEmailInput);
                templateResponse = _mapper.Map<EmailResponseDto<EmailTemplateDto>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while updating email channel: ", ex.Message);
                templateResponse.Message = "Error occurred while updating email channel: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }


        public EmailResponseDto<string> DeleteEmailTemplate(string templateID)
        {
            _logger.LogInformation("DeleteEmailTemplate interactor method.");
            _logger.LogDebug("Template ID:" + templateID);
            EmailResponseDto<string> templateResponse = new EmailResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete Email Template.");
                templateResponse = _emailTemplateRepository.DeleteEmailTemplate(templateID);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while deleting email template: ", ex.Message);
                templateResponse.Message = "Error occurred while deleting email template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }
        #endregion

        #region Email Histories
        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistories(string channelID, string tag)
        {
            _logger.LogInformation("GetEmailHistories interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            EmailResponseDto<List<EmailHistoryDto>> historiesResponse = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                var channelKey = _emailChannelRepository.GetEmailChannelByID(channelID)?.Result?.Key;
                if (!string.IsNullOrEmpty(channelKey))
                {
                    _logger.LogInformation("Trying to get Email histories.");
                    var response = _emailHistoryRepository.GetEmailHistoriesByTag(channelKey, tag);
                    historiesResponse = _mapper.Map<EmailResponseDto<List<EmailHistoryDto>>>(response);
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return historiesResponse;
                }
                else
                {
                    historiesResponse.Status = false;
                    historiesResponse.Message = "Could not get the channel data.";
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return historiesResponse;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email histories: ", ex.Message);
                historiesResponse.Message = "Error occurred while getting email histories: " + ex.Message;
                historiesResponse.Status = false;
                return historiesResponse;
            }
        }
        #endregion
    }
}
