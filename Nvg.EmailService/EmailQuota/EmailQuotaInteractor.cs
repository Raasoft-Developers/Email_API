using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmailQuotaInteractor> _logger;

        public EmailQuotaInteractor(IMapper mapper, IEmailQuotaRepository emailQuotaRepository,
            IEmailChannelRepository emailChannelRepository, ILogger<EmailQuotaInteractor> logger)
        {
            _mapper = mapper;
            _emailQuotaRepository = emailQuotaRepository;
            _emailChannelRepository = emailChannelRepository;
            _logger = logger;
        }

        public EmailResponseDto<EmailQuotaDto> GetEmailQuota(string channelKey)
        {
            _logger.LogInformation("GetEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.GetEmailQuota(channelKey);
                _logger.LogDebug("Status: "+emailQuotaResponse.Status+", Message: " + emailQuotaResponse.Message);
                var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
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

        public EmailResponseDto<EmailQuotaDto> UpdateEmailQuota(string channelKey)
        {
            _logger.LogInformation("UpdateEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var channelID = _emailChannelRepository.GetEmailChannelByKey(channelKey)?.Result?.ID;
                var emailQuotaResponse = _emailQuotaRepository.UpdateEmailQuota(channelID);
                _logger.LogDebug("Status: "+emailQuotaResponse.Status+"Message:" + emailQuotaResponse.Message);
                var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Update Email Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
