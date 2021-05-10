using AutoMapper;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailTemplate;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.EmailTemplate
{
    public class EmailTemplateInteractor : IEmailTemplateInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailPoolRepository _emailPoolRepository;

        public EmailTemplateInteractor(IMapper mapper, IEmailTemplateRepository emailTemplateRepository, IEmailPoolRepository emailPoolRepository)
        {
            _mapper = mapper;
            _emailTemplateRepository = emailTemplateRepository;
            _emailPoolRepository = emailPoolRepository;
        }

        public EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput)
        {
            var response = new EmailResponseDto<EmailTemplateDto>();
            if (!string.IsNullOrEmpty(templateInput.EmailPoolName))
            {
                if (string.IsNullOrEmpty(templateInput.EmailPoolID))
                {
                    var emailPool = _emailPoolRepository.GetEmailPoolByName(templateInput.EmailPoolName)?.Result;
                    if (emailPool != null)
                        templateInput.EmailPoolID = emailPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email pool.";
                        response.Result = templateInput;
                        return response;
                    }
                }
                else
                {
                    var emailPool = _emailPoolRepository.CheckIfEmailPoolIDNameValid(templateInput.EmailPoolID, templateInput.EmailPoolName);
                    if (!emailPool.Status)
                    {
                        response.Status = false;
                        response.Message = "Email Pool ID and Name do not match.";
                        response.Result = templateInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(templateInput.EmailPoolID))
            {
                var emailPool = _emailPoolRepository.CheckIfEmailPoolIDIsValid(templateInput.EmailPoolID);
                if (!emailPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid Email Pool ID.";
                    response.Result = templateInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Email pool cannot be blank.";
                response.Result = templateInput;
                return response;
            }
            var mappedInput = _mapper.Map<EmailTemplateTable>(templateInput);
            var mappedResponse = _emailTemplateRepository.AddEmailTemplate(mappedInput);
            response = _mapper.Map<EmailResponseDto<EmailTemplateDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<EmailTemplateDto> UpdateEmailTemplate(EmailTemplateDto templateInput)
        {
            var response = new EmailResponseDto<EmailTemplateDto>();
            if (!string.IsNullOrEmpty(templateInput.EmailPoolName))
            {
                if (string.IsNullOrEmpty(templateInput.EmailPoolID))
                {
                    var emailPool = _emailPoolRepository.GetEmailPoolByName(templateInput.EmailPoolName)?.Result;
                    if (emailPool != null)
                        templateInput.EmailPoolID = emailPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid Email pool.";
                        response.Result = templateInput;
                        return response;
                    }
                }
                else
                {
                    var emailPool = _emailPoolRepository.CheckIfEmailPoolIDNameValid(templateInput.EmailPoolID, templateInput.EmailPoolName);
                    if (!emailPool.Status)
                    {
                        response.Status = false;
                        response.Message = "Email Pool ID and Name do not match.";
                        response.Result = templateInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(templateInput.EmailPoolID))
            {
                var emailPool = _emailPoolRepository.CheckIfEmailPoolIDIsValid(templateInput.EmailPoolID);
                if (!emailPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid Email Pool ID.";
                    response.Result = templateInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Email pool cannot be blank.";
                response.Result = templateInput;
                return response;
            }
            var mappedInput = _mapper.Map<EmailTemplateTable>(templateInput);
            var mappedResponse = _emailTemplateRepository.UpdateEmailTemplate(mappedInput);
            response = _mapper.Map<EmailResponseDto<EmailTemplateDto>>(mappedResponse);
            return response;
        }

        public EmailResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName)
        {
            var response = _emailTemplateRepository.CheckIfTemplateExist(channelKey, templateName);
            return response;
        }

        public EmailTemplateDto GetEmailTemplate(string templateID)
        {
            var emailTemplate = _emailTemplateRepository.GetEmailTemplate(templateID);
            return _mapper.Map<EmailTemplateDto>(emailTemplate);
        }

        public EmailTemplateDto GetEmailTemplate(string templateName, string channelKey, string variant = null)
        {
            var emailTemplate = _emailTemplateRepository.GetEmailTemplate(templateName, channelKey, variant);
            return _mapper.Map<EmailTemplateDto>(emailTemplate);
        }

    }
}
