using AutoMapper;
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


        public EmailHistoryInteractor(IMapper mapper, IEmailHistoryRepository emailHistoryRepository,
            IEmailProviderRepository emailProviderRepository, IEmailChannelRepository emailChannelRepository)
        {
            _mapper = mapper;
            _emailHistoryRepository = emailHistoryRepository;
            _emailProviderRepository = emailProviderRepository;
            _emailChannelRepository = emailChannelRepository;
        }

        public EmailResponseDto<EmailHistoryDto> AddEmailHistory(EmailHistoryDto historyInput)
        {
            var response = new EmailResponseDto<EmailHistoryDto>();
            if (!string.IsNullOrEmpty(historyInput.ProviderName))
            {
                if (string.IsNullOrEmpty(historyInput.EmailProviderID))
                    historyInput.EmailProviderID = _emailProviderRepository.GetEmailProviderByName(historyInput.ProviderName)?.Result?.ID;
            }
            if (!string.IsNullOrEmpty(historyInput.ChannelKey))
            {
                if (string.IsNullOrEmpty(historyInput.EmailChannelID))
                    historyInput.EmailChannelID = _emailChannelRepository.GetEmailChannelByKey(historyInput.ChannelKey)?.Result?.ID;
            }
            var mappedEmailInput = _mapper.Map<EmailHistoryTable>(historyInput);
            var mappedResponse = _emailHistoryRepository.AddEmailHistory(mappedEmailInput);
            response = _mapper.Map<EmailResponseDto<EmailHistoryDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            var histories = _emailHistoryRepository.GetEmailHistoriesByTag(channelKey, tag);
            return _mapper.Map<EmailResponseDto<List<EmailHistoryDto>>>(histories);
        }
    }
}
