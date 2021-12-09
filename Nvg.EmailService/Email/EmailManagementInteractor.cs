using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailHistory;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.EmailQuota;
using Nvg.EmailService.Data.EmailTemplate;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailProvider;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailManagementInteractor : IEmailManagementInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailEventInteractor _emailEventInteractor;
        private readonly IEmailPoolRepository _emailPoolRepository;
        private readonly IEmailProviderRepository _emailProviderRepository;
        private readonly IEmailChannelRepository _emailChannelRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailProviderInteractor _emailProviderInteractor;
        private readonly IEmailHistoryRepository _emailHistoryRepository;
        private readonly IEmailQuotaRepository _emailQuotaRepository;
        private readonly ILogger<EmailManagementInteractor> _logger;

        public EmailManagementInteractor(IMapper mapper, IEmailEventInteractor emailEventInteractor, IEmailPoolRepository emailPoolRepository, IEmailProviderRepository emailProviderRepository,
            IEmailChannelRepository emailChannelRepository, IEmailTemplateRepository emailTemplateRepository, IEmailProviderInteractor emailProviderInteractor,
            IEmailHistoryRepository emailHistoryRepository, IEmailQuotaRepository emailQuotaRepository, ILogger<EmailManagementInteractor> logger)
        {
            _mapper = mapper;
            _emailEventInteractor = emailEventInteractor;
            _emailPoolRepository = emailPoolRepository;
            _emailProviderRepository = emailProviderRepository;
            _emailChannelRepository = emailChannelRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _emailProviderInteractor = emailProviderInteractor;
            _emailHistoryRepository = emailHistoryRepository;
            _emailQuotaRepository = emailQuotaRepository;
            _logger = logger;
        }
        #region Email Pool

        public EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto poolInput)
        {
            _logger.LogInformation("AddEmailPool interactor method.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailPool.");
                var mappedEmailInput = _mapper.Map<EmailPoolTable>(poolInput);
                var response = _emailPoolRepository.AddEmailPool(mappedEmailInput);
                poolResponse = _mapper.Map<EmailResponseDto<EmailPoolDto>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email pool: ", ex.Message);
                poolResponse.Message = "Error occurred while adding email pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

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

        public EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddEmailProvider interactor method.");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailProvider.");
                var mappedEmailInput = _mapper.Map<EmailProviderSettingsTable>(providerInput);
                var response = _emailProviderRepository.AddEmailProvider(mappedEmailInput);
                providerResponse = _mapper.Map<EmailResponseDto<EmailProviderSettingsDto>>(response);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in Email Interactor while adding email provider: ", ex.Message);
                providerResponse.Message = "Error occurred while updating email provider: " + ex.Message;
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
                var mappedEmailInput = _mapper.Map<EmailProviderSettingsTable>(providerInput);
                var response = _emailProviderRepository.UpdateEmailProvider(mappedEmailInput);
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
            _logger.LogInformation("GetEmailChannelsByPool interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            EmailResponseDto<List<EmailChannelDto>> channelResponse = new EmailResponseDto<List<EmailChannelDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Channels.");
                var response = _emailChannelRepository.GetEmailChannels(poolID);
                //channelResponse = _mapper.Map<EmailResponseDto<List<EmailChannelDto>>>(response);
                _logger.LogDebug("Status: " + response.Status + ", " + response.Message);
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email channel: ", ex.Message);
                channelResponse.Message = "Error occurred while getting email channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("AddEmailChannel interactor method.");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailChannel.");
                var mappedEmailInput = _mapper.Map<EmailChannelTable>(channelInput);
                var response = _emailChannelRepository.AddEmailChannel(mappedEmailInput);
                if (response.Status && channelInput.IsRestrictedByQuota)
                {
                    channelInput.ID = response.Result.ID;
                    //If channel has been added and channel isRestrictedByQuota, add email quota for channel
                    _emailQuotaRepository.AddEmailQuota(channelInput);
                }
                channelResponse = _mapper.Map<EmailResponseDto<EmailChannelDto>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
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
                var mappedEmailInput = _mapper.Map<EmailChannelTable>(channelInput);
                var response = _emailChannelRepository.UpdateEmailChannel(mappedEmailInput);
                var quotaResponse = _emailQuotaRepository.UpdateEmailQuota(channelInput);
                if (!response.Status)
                {
                    //if email channel is not updated , then take response of email quota updation
                    response.Status = quotaResponse.Status;
                    response.Message = quotaResponse.Message;
                }
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
                var quotaResponse = _emailQuotaRepository.DeleteEmailQuota(channelID);
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
        public EmailResponseDto<List<EmailTemplateDto>> GetEmailTemplatesByChannelID(string channelID)
        {
            _logger.LogInformation("GetEmailTemplatesByChannelID interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            EmailResponseDto<List<EmailTemplateDto>> templateResponse = new EmailResponseDto<List<EmailTemplateDto>>();
            try
            {
                _logger.LogInformation("Trying to get Email Templates.");
                var poolID = _emailChannelRepository.GetEmailChannelByID(channelID)?.Result?.EmailPoolID;
                if (!string.IsNullOrEmpty(poolID)){
                    var response = _emailTemplateRepository.GetEmailTemplatesByPool(poolID);
                    templateResponse = _mapper.Map<EmailResponseDto<List<EmailTemplateDto>>>(response);                    
                }
                else
                {
                    templateResponse.Message = "Channel does not exist.";
                    templateResponse.Status = false;
                }
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
        
        public EmailResponseDto<EmailTemplateDto> GetEmailTemplate(string templateID)
        {
            _logger.LogInformation("GetEmailTemplate interactor method.");
            _logger.LogDebug("Template ID:" + templateID);
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to get Email Templates.");
                var response = _emailTemplateRepository.GetEmailTemplate(templateID);
                if (response != null)
                {
                    templateResponse.Status = true;
                    templateResponse.Message = "Successfully retrieved data";
                    templateResponse.Result = _mapper.Map<EmailTemplateDto>(response);
                }
                else
                {
                    templateResponse.Status = true;
                    templateResponse.Message = "Template not found";
                }
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in Email Management Interactor while getting email template by id: ", ex.Message);
                templateResponse.Message = "Error occurred while getting email template by id: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("AddEmailTemplate interactor method.");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to add EmailTemplate.");
                var mappedEmailInput = _mapper.Map<EmailTemplateTable>(templateInput);
                var response = _emailTemplateRepository.AddEmailTemplate(mappedEmailInput);
                templateResponse = _mapper.Map<EmailResponseDto<EmailTemplateDto>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
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
                var mappedEmailInput = _mapper.Map<EmailTemplateTable>(templateInput);
                var response = _emailTemplateRepository.UpdateEmailTemplate(mappedEmailInput);
                templateResponse = _mapper.Map<EmailResponseDto<EmailTemplateDto>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
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

        #region Email Quota
        public EmailResponseDto<List<EmailQuotaDto>> GetEmailQuotaList(string channelID)
        {
            _logger.LogInformation("GetEmailQuota interactor method.");
            var response = new EmailResponseDto<List<EmailQuotaDto>>();
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.GetEmailQuotaList(channelID);
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + ", Message: " + emailQuotaResponse.Message);
                var mappedResponse = _mapper.Map<EmailResponseDto<List<EmailQuotaDto>>>(emailQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get Email Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailQuotaDto> AddEmailQuota(EmailChannelDto emailChannelDto)
        {
            _logger.LogInformation("AddEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                if (emailChannelDto.IsRestrictedByQuota && (emailChannelDto.MonthlyQuota == 0 || emailChannelDto.TotalQuota == 0))
                {
                    response.Status = false;
                    response.Message = "Monthly quota and/or Total quota cannot have value as 0. Quota has not been updated in the database.";
                    _logger.LogDebug("Status: " + response.Status + "Message:" + response.Message);
                    return response;
                }
                emailChannelDto.ID = _emailChannelRepository.GetEmailChannelByKey(emailChannelDto.Key)?.Result?.ID;
                if (!string.IsNullOrEmpty(emailChannelDto.ID))
                {
                    var emailQuotaResponse = _emailQuotaRepository.AddEmailQuota(emailChannelDto);
                    _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);
                    var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                    return mappedResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Invalid Channel Key.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Add Email Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailQuotaDto> UpdateEmailQuota(EmailChannelDto emailChannelDto)
        {
            _logger.LogInformation("UpdateEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                if (emailChannelDto.IsRestrictedByQuota && (emailChannelDto.MonthlyQuota == 0 || emailChannelDto.TotalQuota == 0))
                {
                    response.Status = false;
                    response.Message = "Monthly quota and/or Total quota cannot have value as 0. Quota has not been updated in the database.";
                    _logger.LogDebug("Status: " + response.Status + "Message:" + response.Message);
                    return response;
                }
                emailChannelDto.ID = _emailChannelRepository.GetEmailChannelByKey(emailChannelDto.Key)?.Result?.ID;
                if (!string.IsNullOrEmpty(emailChannelDto.ID))
                {
                    var emailQuotaResponse = _emailQuotaRepository.UpdateEmailQuota(emailChannelDto);
                    _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);

                    var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                    return mappedResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Invalid Channel Key.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Update Email Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<string> DeleteEmailQuota(string channelID)
        {
            _logger.LogInformation("DeleteEmailQuota interactor method.");
            var response = new EmailResponseDto<string>();
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.DeleteEmailQuota(channelID);
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);
                //var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                return emailQuotaResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Add Email Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        #endregion
        public EmailResponseDto<string> SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation("SendMail interactor method.");
            var response = new EmailResponseDto<string>();
            try
            {
                emailInputs.Recipients.RemoveAll(x => string.IsNullOrEmpty(x));
                if (emailInputs.Recipients.Count == 0)
                {
                    _logger.LogError("Recipient cannot be null or empty.");
                    response.Status = false;
                    response.Message = "Recipient cannot be null or empty.";
                    return response;
                }
                if (string.IsNullOrEmpty(emailInputs.Subject))
                {
                    _logger.LogError("Subject is mandatory.");
                    response.Status = false;
                    response.Message = "Subject is mandatory.";
                    return response;
                }
                if (string.IsNullOrEmpty(emailInputs.ChannelKey))
                {
                    _logger.LogError("Channel key cannot be blank.");
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _emailChannelRepository.CheckIfChannelExist(emailInputs.ChannelKey).Result;
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
                    var templateExist = _emailTemplateRepository.CheckIfTemplateExist(emailInputs.ChannelKey, emailInputs.TemplateName).Result;
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
                response.Message = $"Email is sent successfully to {string.Join(",", emailInputs.Recipients)}.";
                _logger.LogDebug("" + response.Message);
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
