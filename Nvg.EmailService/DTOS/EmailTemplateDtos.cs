using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System.Collections.Generic;

namespace Nvg.EmailService.DTOS
{
    public class EmailTemplateDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string EmailPoolID { get; set; }
        public string EmailPoolName { get; set; }
        public string MessageTemplate { get; set; }
    }

    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplateTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailTemplateTable>, EmailResponseDto<EmailTemplateDto>>();
            CreateMap<EmailResponseDto<List<EmailTemplateTable>>, EmailResponseDto<List<EmailTemplateDto>>>();
        }
    }
}
