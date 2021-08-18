using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailHistory;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailHistory
{
    public class EmailHistoryInteractor : IEmailHistoryInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailHistoryRepository _emailHistoryRepository;
        private readonly IEmailProviderRepository _emailProviderRepository;
        private readonly IEmailChannelRepository _emailChannelRepository;
        private readonly ILogger<EmailHistoryInteractor> _logger;

        public EmailHistoryInteractor(IMapper mapper, IEmailHistoryRepository emailHistoryRepository,
            IEmailProviderRepository emailProviderRepository, IEmailChannelRepository emailChannelRepository, ILogger<EmailHistoryInteractor> logger)
        {
            _mapper = mapper;
            _emailHistoryRepository = emailHistoryRepository;
            _emailProviderRepository = emailProviderRepository;
            _emailChannelRepository = emailChannelRepository;
            _logger = logger;
        }

        public EmailResponseDto<EmailHistoryDto> AddEmailHistory(EmailHistoryDto historyInput)
        {
            _logger.LogInformation("AddEmailHistory interactor method.");
            var response = new EmailResponseDto<EmailHistoryDto>();
            try
            {               
                if (!string.IsNullOrEmpty(historyInput.ProviderName))
                {
                    _logger.LogDebug($"Providername: {historyInput.ProviderName}");
                    if (string.IsNullOrEmpty(historyInput.EmailProviderID))
                    {
                        historyInput.EmailProviderID = _emailProviderRepository.GetEmailProviderByName(historyInput.ProviderName)?.Result?.ID;
                        _logger.LogDebug($"EmailProviderID: {historyInput.EmailProviderID}");
                    }
                    else
                    {
                        var emailProvider = _emailProviderRepository.CheckIfEmailProviderIDNameValid(historyInput.EmailProviderID, historyInput.ProviderName);
                        if (!emailProvider.Status)
                        {
                            response.Status = false;
                            response.Message = "Email Provider ID and Provider Name do not match.";
                            response.Result = historyInput;
                            return response;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(historyInput.EmailProviderID))
                {
                    var emailProvider = _emailProviderRepository.CheckIfEmailProviderIDIsValid(historyInput.EmailProviderID);
                    if (!emailProvider.Status)
                    {
                        response.Status = false;
                        response.Message = "Invalid Email Provider ID.";
                        response.Result = historyInput;
                        return response;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Provider ID or Name cannot be blank.";
                    response.Result = historyInput;
                    _logger.LogError($"{response.Message}");
                    return response;
                }
                if (!string.IsNullOrEmpty(historyInput.ChannelKey))
                {
                    _logger.LogDebug($"ChannelKey: {historyInput.ChannelKey}");
                    if (string.IsNullOrEmpty(historyInput.EmailChannelID))
                    {
                        historyInput.EmailChannelID = _emailChannelRepository.GetEmailChannelByKey(historyInput.ChannelKey)?.Result?.ID;
                        _logger.LogDebug($"EmailChannelID: {historyInput.EmailChannelID}");
                    }
                    else
                    {
                        var emailChannel = _emailChannelRepository.CheckIfEmailChannelIDKeyValid(historyInput.EmailChannelID, historyInput.ChannelKey);
                        if (!emailChannel.Status)
                        {
                            _logger.LogError($"{emailChannel.Message}");
                            response.Status = false;
                            response.Message = emailChannel.Message;
                            response.Result = historyInput;
                            return response;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(historyInput.EmailChannelID))
                {
                    var emailChannel = _emailChannelRepository.CheckIfEmailChannelIDIsValid(historyInput.EmailChannelID);
                    if (!emailChannel.Status)
                    {
                        _logger.LogError($"{emailChannel.Message}");
                        response.Status = false;
                        response.Message = emailChannel.Message;
                        response.Result = historyInput;
                        return response;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Channel ID or Key cannot be blank.";
                    response.Result = historyInput;
                    _logger.LogError($"{response.Message}");
                    return response;
                }
                var mappedEmailInput = _mapper.Map<EmailHistoryTable>(historyInput);
                var mappedResponse = _emailHistoryRepository.AddEmailHistory(mappedEmailInput);
                response = _mapper.Map<EmailResponseDto<EmailHistoryDto>>(mappedResponse);
                _logger.LogDebug($"Status: {response.Status}, Message: {response.Message}");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred while adding email history:" + ex.Message);
                response.Message = "Error occurred while adding email history: " + ex.Message;
                response.Status = false;
                return response;
            }
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate)
        {
            _logger.LogInformation("GetEmailHistoriesByDateRange interactor method.");
            EmailResponseDto<List<EmailHistoryDto>> responseDto = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                var histories = _emailHistoryRepository.GetEmailHistoriesByDateRange(channelKey, tag, fromDate, toDate);
                return _mapper.Map<EmailResponseDto<List<EmailHistoryDto>>>(histories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting email history by date range:" + ex.Message);
                responseDto.Message = "Failed to get histories by date range: " + ex.Message;
                responseDto.Status = false;
                return responseDto;
            }
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("AddEmailHistory interactor method.");
            EmailResponseDto<List<EmailHistoryDto>> responseDto = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                var histories = _emailHistoryRepository.GetEmailHistoriesByTag(channelKey, tag);
                _logger.LogDebug($"Status: {histories.Status},Message: {histories.Message}");
                return _mapper.Map<EmailResponseDto<List<EmailHistoryDto>>>(histories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting email history by tag:" + ex.Message);
                responseDto.Message = "Failed to get histories by tag: " + ex.Message;
                responseDto.Status = false;
                return responseDto;
            }
        }
    }
}
