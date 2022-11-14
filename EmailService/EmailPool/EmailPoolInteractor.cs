using AutoMapper;
using EmailService.Data.EmailPool;
using EmailService.Data.Entities;
using EmailService.DTOS;

namespace EmailService.EmailPool
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
            var mappedEmailResponse = _mapper.Map<EmailResponseDto<EmailPoolDto>>(response);
            return mappedEmailResponse;
        }
    }
}
