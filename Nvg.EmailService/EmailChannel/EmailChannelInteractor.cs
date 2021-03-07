using AutoMapper;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.EmailChannel
{
    public class EmailChannelInteractor : IEmailChannelInteractor
    {
        private readonly IEmailChannelRepository _emailChannelRepository;
        private readonly IMapper _mapper;
        private readonly IEmailProviderRepository _emailProviderRepository;
        private readonly IEmailPoolRepository _emailPoolRepository;
        private readonly string defaultEmailProvider = "MasterEmailProvider";

        public EmailChannelInteractor(IMapper mapper, IEmailChannelRepository emailChannelRepository, IEmailProviderRepository emailProviderRepository,
            IEmailPoolRepository emailPoolRepository)
        {
            _emailChannelRepository = emailChannelRepository;
            _mapper = mapper;
            _emailProviderRepository = emailProviderRepository;
            _emailPoolRepository = emailPoolRepository;
        }

        public EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput)
        {
            var response = new EmailResponseDto<EmailChannelDto>();
            if (!string.IsNullOrEmpty(channelInput.EmailPoolName))
            {
                if (string.IsNullOrEmpty(channelInput.EmailPoolID))
                {
                    var emailPool = _emailPoolRepository.GetEmailPoolByName(channelInput.EmailPoolName)?.Result;
                    if (emailPool != null)
                        channelInput.EmailPoolID = emailPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email pool.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }
            else if(string.IsNullOrEmpty(channelInput.EmailPoolID))
            {
                response.Status = false;
                response.Message = "Email pool cannot be blank.";
                response.Result = channelInput;
                return response;
            }
            if (!string.IsNullOrEmpty(channelInput.EmailProviderName))
            {
                if (string.IsNullOrEmpty(channelInput.EmailProviderID))
                {
                    var emailProvider = _emailProviderRepository.GetEmailProviderByName(channelInput.EmailProviderName)?.Result;
                    if (emailProvider != null)
                        channelInput.EmailProviderID = emailProvider.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email provider.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }
            else
                channelInput.EmailProviderID = _emailProviderRepository.GetEmailProviderByName(defaultEmailProvider)?.Result?.ID;

            var mappedEmailInput = _mapper.Map<EmailChannelTable>(channelInput);
            var mappedResponse = _emailChannelRepository.AddEmailChannel(mappedEmailInput);
            response = _mapper.Map<EmailResponseDto<EmailChannelDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey)
        {
            var response = _emailChannelRepository.GetEmailChannelByKey(channelKey);
            var mappedResponse = _mapper.Map<EmailResponseDto<EmailChannelDto>>(response);
            return mappedResponse;
        }

        public EmailResponseDto<bool> CheckIfChannelExist(string channelKey)
        {
            var response = _emailChannelRepository.CheckIfChannelExist(channelKey);
            return response;
        }
    }
}
