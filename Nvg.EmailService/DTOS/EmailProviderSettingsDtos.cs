using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System.Collections.Generic;

namespace Nvg.EmailService.DTOS
{
    public class EmailProviderSettingsDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string EmailPoolName { get; set; }
        public string EmailPoolID { get; set; }
    }

    public class EmailProviderSettingsProfile : Profile
    {
        public EmailProviderSettingsProfile()
        {
            CreateMap<EmailProviderSettingsDto, EmailProviderSettingsTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailProviderSettingsTable>, EmailResponseDto<EmailProviderSettingsDto>>();
            CreateMap<EmailResponseDto<List<EmailProviderSettingsTable>>, EmailResponseDto<List<EmailProviderSettingsDto>>>();

        }
    }
}
