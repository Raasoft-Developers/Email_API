using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailErrorLog;
using Nvg.EmailService.Data.EmailHistory;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailErrorLog
{
    public class EmailErrorLogInteractor : IEmailErrorLogInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailErrorLogRepository _emailErrorLogRepository;
        private readonly IEmailChannelRepository _emailChannelRepository;
        private readonly ILogger<EmailErrorLogInteractor> _logger;

        public EmailErrorLogInteractor(IMapper mapper, IEmailErrorLogRepository emailErrorLogRepository, IEmailChannelRepository emailChannelRepository, ILogger<EmailErrorLogInteractor> logger)
        {
            _mapper = mapper;
            _emailErrorLogRepository = emailErrorLogRepository;
            _emailChannelRepository = emailChannelRepository;
            _logger = logger;
        }

        public EmailResponseDto<EmailErrorLogTable> AddEmailErrorLog(EmailErrorLogTable errorLogInput)
        {
            _logger.LogInformation("AddEmailErrorLog interactor method.");
            var response = new EmailResponseDto<EmailErrorLogTable>();
            try
            {
                if (!string.IsNullOrEmpty(errorLogInput.ChannelKey))
                {
                    errorLogInput.EmailChannelID = _emailChannelRepository.GetEmailChannelByKey(errorLogInput.ChannelKey)?.Result?.ID;
                    _logger.LogDebug($"EmailChannelID: {errorLogInput.EmailChannelID}");
                }
                var mappedResponse = _emailErrorLogRepository.AddEmailErrorLog(errorLogInput);
                response = _mapper.Map<EmailResponseDto<EmailErrorLogTable>>(mappedResponse);
                _logger.LogDebug($"Status: {response.Status}, Message: {response.Message}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while adding email history:" + ex.Message);
                response.Message = "Error occurred while adding email history: " + ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
