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
        public bool CheckIfQuotaExceeded(string channelKey)
        {
            _logger.LogInformation("GetEmailQuota interactor method.");
            var response = false;
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.GetEmailQuota(channelKey);
                if (emailQuotaResponse.Status)
                {
                    var emailQuota = emailQuotaResponse.Result;
                    var currentMonth = DateTime.Now.ToString("MMM").ToUpper();
                    //Check if Quota is set for current month
                    if(emailQuota.CurrentMonth == currentMonth)
                    {
                        //Check if quota is exceeded for current month
                        if (emailQuota.MonthlyQuota != -1 && emailQuota.TotalQuota != -1 && emailQuota.MonthlyConsumption >= emailQuota.MonthlyQuota && emailQuota.TotalConsumption >= emailQuota.TotalConsumption)
                        {
                            response = true;
                        }
                    }
                    else
                    {   //Reset Quota for Current month and Update the Current Month
                        var updatedQuotaResponse = _emailQuotaRepository.UpdateCurrentMonth(channelKey, currentMonth);
                        if (updatedQuotaResponse.Status)
                        {
                            _logger.LogDebug("Status: " + updatedQuotaResponse.Status + ", Message: " + updatedQuotaResponse.Message);
                            emailQuota = updatedQuotaResponse.Result;
                            //Check if quota is exceeded for current month
                            if (emailQuota.MonthlyQuota != -1 && emailQuota.TotalQuota != -1 && emailQuota.MonthlyConsumption > emailQuota.MonthlyQuota && emailQuota.TotalConsumption > emailQuota.TotalConsumption)
                            {
                                response = true;
                            }
                        }
                    }
                }
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + ", Message: " + emailQuotaResponse.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get Email Quota" + ex.Message);
                response = true;
                return response;
            }
        }
        public EmailResponseDto<EmailQuotaDto> AddEmailQuota(EmailChannelDto emailChannelDto)
        {
            _logger.LogInformation("UpdateEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var channelID = _emailChannelRepository.GetEmailChannelByKey(emailChannelDto.Key)?.Result?.ID;
                var emailQuotaResponse = _emailQuotaRepository.AddEmailQuota(emailChannelDto);
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);
                var mappedResponse = _mapper.Map<EmailResponseDto<EmailQuotaDto>>(emailQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Add Email Quota" + ex.Message);
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
