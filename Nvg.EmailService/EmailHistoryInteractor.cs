using AutoMapper;
using Nvg.EmailService.Data;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService
{
    public class EmailHistoryInteractor : IEmailHistoryInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailHistoryRepository _emailHistoryRepository;

        public EmailHistoryInteractor(IMapper mapper, IEmailHistoryRepository emailHistoryRepository)
        {
            _mapper = mapper;
            _emailHistoryRepository = emailHistoryRepository;
        }

        public EmailHistoryDto Add(EmailHistoryDto email)
        {
            var emailObj = _mapper.Map<EmailHistoryModel>(email);
            emailObj = _emailHistoryRepository.Add(emailObj);
            return _mapper.Map<EmailHistoryDto>(emailObj);
        }

    }
}
