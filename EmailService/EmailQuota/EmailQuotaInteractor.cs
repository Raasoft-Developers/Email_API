using AutoMapper;
using EmailService.Data.EmailChannel;
using EmailService.Data.EmailQuota;
using EmailService.DTOS;
using Microsoft.Extensions.Logging;
using System;

namespace EmailService.EmailQuota
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
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + ", Message: " + emailQuotaResponse.Message);
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
        public EmailBalanceDto CheckIfQuotaExceeded(string channelKey)
        {
            _logger.LogInformation("CheckIfQuotaExceeded interactor method.");
            var response = new EmailBalanceDto();
            try
            {
                var emailQuotaResponse = _emailQuotaRepository.GetEmailQuota(channelKey);
                if (emailQuotaResponse.Status && emailQuotaResponse.Result != null)
                {
                    var emailQuota = emailQuotaResponse.Result;
                    var currentMonth = DateTime.Now.ToString("MMM").ToUpper();
                    //Check if Current Month in Table is set to the actual current month else update the table value
                    if (emailQuota.CurrentMonth != currentMonth)
                    {
                        var updatedQuotaResponse = _emailQuotaRepository.UpdateCurrentMonth(channelKey, currentMonth);
                        if (updatedQuotaResponse.Status)
                        {
                            _logger.LogDebug("Status: " + updatedQuotaResponse.Status + ", Message: " + updatedQuotaResponse.Message);
                            emailQuota = updatedQuotaResponse.Result;
                        }
                    }
                    if (emailQuota.TotalQuota != -1)
                    {
                        response.HasLimit = true;
                        response.Balance = emailQuota.TotalQuota - emailQuota.TotalConsumption;
                        //Replace Balance with the lower value from the Total and Monthly Balance
                        var monthlyBalance = emailQuota.MonthlyQuota - emailQuota.MonthlyConsumption;
                        if (response.Balance > monthlyBalance)
                        {
                            response.Balance = monthlyBalance;
                        }
                        if (emailQuota.MonthlyConsumption >= emailQuota.MonthlyQuota || emailQuota.TotalConsumption >= emailQuota.TotalQuota)
                        {
                            response.IsExceeded = true;
                        }
                    }
                }
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + ", Message: " + emailQuotaResponse.Message);
                _logger.LogDebug("IsExceed: " + response.IsExceeded + ", Balance: " + response.Balance + ", HasLimit: " + response.HasLimit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get Email Quota" + ex.Message);
                response.IsExceeded = true;
                return response;
            }
        }
        public EmailResponseDto<EmailQuotaDto> AddEmailQuota(EmailChannelDto emailChannelDto)
        {
            _logger.LogInformation("AddEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
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
        public EmailResponseDto<EmailQuotaDto> IncrementEmailQuota(string channelKey)
        {
            _logger.LogInformation("UpdateEmailQuota interactor method.");
            var response = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                var channelID = _emailChannelRepository.GetEmailChannelByKey(channelKey)?.Result?.ID;
                var emailQuotaResponse = _emailQuotaRepository.IncrementEmailQuota(channelID);
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);
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
                var emailQuotaResponse = _emailQuotaRepository.UpdateEmailQuota(emailChannelDto);
                _logger.LogDebug("Status: " + emailQuotaResponse.Status + "Message:" + emailQuotaResponse.Message);
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
