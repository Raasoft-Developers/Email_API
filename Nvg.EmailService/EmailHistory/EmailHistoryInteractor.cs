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
            _logger.LogInformation("In EmailHistoryInteractor: AddEmailHistory interactor method hit.");
            var response = new EmailResponseDto<EmailHistoryDto>();
            try
            {               
                if (!string.IsNullOrEmpty(historyInput.ProviderName))
                {
                    _logger.LogDebug($"In EmailHistoryInteractor: {historyInput.ProviderName}");
                    if (string.IsNullOrEmpty(historyInput.EmailProviderID))
                    {
                        historyInput.EmailProviderID = _emailProviderRepository.GetEmailProviderByName(historyInput.ProviderName)?.Result?.ID;
                        _logger.LogDebug($"In EmailHistoryInteractor: {historyInput.EmailProviderID}");
                    }
                }
                if (!string.IsNullOrEmpty(historyInput.ChannelKey))
                {
                    _logger.LogDebug($"In EmailHistoryInteractor: {historyInput.ChannelKey}");
                    if (string.IsNullOrEmpty(historyInput.EmailChannelID))
                    {
                        historyInput.EmailChannelID = _emailChannelRepository.GetEmailChannelByKey(historyInput.ChannelKey)?.Result?.ID;
                        _logger.LogDebug($"In EmailHistoryInteractor: {historyInput.EmailChannelID}");
                    }
                }
                var mappedEmailInput = _mapper.Map<EmailHistoryTable>(historyInput);
                var mappedResponse = _emailHistoryRepository.AddEmailHistory(mappedEmailInput);
                response = _mapper.Map<EmailResponseDto<EmailHistoryDto>>(mappedResponse);
                _logger.LogDebug($"In EmailHistoryInteractor: Successfully added Email History.");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("In EmailHistoryInteractor: Error occurred while adding email history:" + ex.Message);
                response.Message = "Error occurred while adding email history: " + ex.Message;
                response.Status = false;
                return response;
            }
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("In EmailHistoryInteractor: AddEmailHistory interactor method hit.");
            EmailResponseDto<List<EmailHistoryDto>> responseDto = new EmailResponseDto<List<EmailHistoryDto>>();
            try
            {
                var histories = _emailHistoryRepository.GetEmailHistoriesByTag(channelKey, tag);
                _logger.LogDebug($"In EmailHistoryInteractor: "+ histories.Message);
                return _mapper.Map<EmailResponseDto<List<EmailHistoryDto>>>(histories);
            }
            catch (Exception ex)
            {
                _logger.LogError("In EmailHistoryInteractor: Error occurred while getting email history by tag:" + ex.Message);
                responseDto.Message = "Failed to get histories by tag: " + ex.Message;
                responseDto.Status = false;
                return responseDto;
            }
        }
    }
}
