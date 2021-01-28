using AutoMapper;
using Nvg.EmailService.Data;
using Nvg.EmailService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService
{
    public class EmailTemplateInteractor : IEmailTemplateInteractor
    {
        private readonly IMapper _mapper;
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailTemplateInteractor(IMapper mapper, IEmailTemplateRepository emailTemplateRepository)
        {
            _mapper = mapper;
            _emailTemplateRepository = emailTemplateRepository;
        }

        public EmailTemplateDto GetEmailTemplate(long id)
        {
            var emailTemplate = _emailTemplateRepository.GetEmailTemplate(id);
            return _mapper.Map<EmailTemplateDto>(emailTemplate);
        }

        public EmailTemplateDto GetEmailTemplate(string templateName, string tenantID = null, string facilityID = null)
        {
            var emailTemplate = _emailTemplateRepository.GetEmailTemplate(templateName, tenantID, facilityID);
            return _mapper.Map<EmailTemplateDto>(emailTemplate);
        }

    }
}
