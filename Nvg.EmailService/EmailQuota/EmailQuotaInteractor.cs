using AutoMapper;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailQuota;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailQuota
{
    public class EmailQuotaInteractor : IEmailQuotaInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailQuotaRepository _emailQuotaRepository;
        private readonly IEmailChannelRepository _emailChannelRepository;

        public EmailQuotaInteractor(IMapper mapper, IEmailQuotaRepository emailQuotaRepository,
            IEmailChannelRepository emailChannelRepository)
        {
            _mapper = mapper;
            _emailQuotaRepository = emailQuotaRepository;
            _emailChannelRepository = emailChannelRepository;
        }

        public EmailResponseDto<EmailQuotaDto> GetEmailQuota(string channelKey)
        {
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.GetEmailQuota(channelKey);
                var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailQuotaDto> UpdateEmailQuota(string channelKey)
        {
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var channelID = _emailChannelRepository.GetEmailChannelByKey(channelKey)?.Result?.ID;
                var emailQuotaResponse = _emailQuotaRepository.UpdateEmailQuota(channelID);
                var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
