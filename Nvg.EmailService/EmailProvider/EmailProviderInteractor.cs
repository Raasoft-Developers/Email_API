using AutoMapper;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailProvider
{
    public class EmailProviderInteractor : IEmailProviderInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailProviderRepository _emailProviderRepository;
        private readonly IEmailPoolRepository _emailPoolRepository;

        public EmailProviderInteractor(IMapper mapper, IEmailProviderRepository emailProviderRepository, IEmailPoolRepository emailPoolRepository)
        {
            _mapper = mapper;
            _emailProviderRepository = emailProviderRepository;
            _emailPoolRepository = emailPoolRepository;
        }

        public EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            var response = new EmailResponseDto<EmailProviderSettingsDto>();
            if (!string.IsNullOrEmpty(providerInput.EmailPoolName))
            {
                if (string.IsNullOrEmpty(providerInput.EmailPoolID))
                {
                    var emailPool = _emailPoolRepository.GetEmailPoolByName(providerInput.EmailPoolName)?.Result;
                    if (emailPool != null)
                        providerInput.EmailPoolID = emailPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email pool.";
                        response.Result = providerInput;
                        return response;
                    }
                }
                else
                {
                    var emailPool = _emailPoolRepository.CheckIfEmailPoolIDNameValid(providerInput.EmailPoolID, providerInput.EmailPoolName);
                    if (!emailPool.Status)
                    {
                        response.Status = false;
                        response.Message = "Email Pool ID and Name do not match.";
                        response.Result = providerInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(providerInput.EmailPoolID))
            {
                var emailPool = _emailPoolRepository.CheckIfEmailPoolIDIsValid(providerInput.EmailPoolID);
                if (!emailPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid Email Pool ID.";
                    response.Result = providerInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Email pool cannot be blank.";
                response.Result = providerInput;
                return response;
            }
            var mappedEmailInput = _mapper.Map<EmailProviderSettingsTable>(providerInput);
            var mappedResponse = _emailProviderRepository.AddEmailProvider(mappedEmailInput);
            response = _mapper.Map<EmailResponseDto<EmailProviderSettingsDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<EmailProviderSettingsDto> UpdateEmailProvider(EmailProviderSettingsDto providerInput)
        {
            var response = new EmailResponseDto<EmailProviderSettingsDto>();
            if (!string.IsNullOrEmpty(providerInput.EmailPoolName))
            {
                if (string.IsNullOrEmpty(providerInput.EmailPoolID))
                {
                    var emailPool = _emailPoolRepository.GetEmailPoolByName(providerInput.EmailPoolName)?.Result;
                    if (emailPool != null)
                        providerInput.EmailPoolID = emailPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email pool.";
                        response.Result = providerInput;
                        return response;
                    }
                }
                else
                {
                    var emailPool = _emailPoolRepository.CheckIfEmailPoolIDNameValid(providerInput.EmailPoolID, providerInput.EmailPoolName);
                    if (!emailPool.Status)
                    {
                        response.Status = false;
                        response.Message = "Email Pool ID and Name do not match.";
                        response.Result = providerInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(providerInput.EmailPoolID))
            {
                var emailPool = _emailPoolRepository.CheckIfEmailPoolIDIsValid(providerInput.EmailPoolID);
                if (!emailPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid Email Pool ID.";
                    response.Result = providerInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Email pool cannot be blank.";
                response.Result = providerInput;
                return response;
            }
            var mappedEmailInput = _mapper.Map<EmailProviderSettingsTable>(providerInput);
            var mappedResponse = _emailProviderRepository.UpdateEmailProvider(mappedEmailInput);
            response = _mapper.Map<EmailResponseDto<EmailProviderSettingsDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<EmailProviderSettingsDto> GetEmailProviderByChannel(string channelKey)
        {
            var provider = _emailProviderRepository.GetEmailProviderByChannelKey(channelKey);
            return _mapper.Map<EmailResponseDto<EmailProviderSettingsDto>>(provider);
        }

        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName)
        {
            var providers = _emailProviderRepository.GetEmailProvidersByPool(poolName, providerName);
            return _mapper.Map<EmailResponseDto<List<EmailProviderSettingsDto>>>(providers);
        }
    }
}
