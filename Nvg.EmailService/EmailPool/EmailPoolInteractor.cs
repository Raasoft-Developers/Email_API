using AutoMapper;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailPool
{
    public class EmailPoolInteractor : IEmailPoolInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailPoolRepository _emailPoolRepository;

        public EmailPoolInteractor(IMapper mapper, IEmailPoolRepository emailPoolRepository)
        {
            _mapper = mapper;
            _emailPoolRepository = emailPoolRepository;
        }

        public EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto emailPoolInput)
        {
            var mappedEmailInput = _mapper.Map<EmailPoolTable>(emailPoolInput);
            var response = _emailPoolRepository.AddEmailPool(mappedEmailInput);
            var mappedSMSResponse = _mapper.Map<EmailResponseDto<EmailPoolDto>>(response);
            return mappedSMSResponse;
        }
    }
}
