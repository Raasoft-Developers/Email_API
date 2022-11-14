using AutoMapper;
using EmailService.Data.Entities;
using System.Collections.Generic;

namespace EmailService.DTOS
{
    public class EmailQuotaDto
    {
        public string ID { get; set; }
        public string EmailChannelID { get; set; }
        public string EmailChannelKey { get; set; }
        public int TotalConsumption { get; set; }
        public int TotalQuota { get; set; }
        public int MonthlyConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public int MonthlyQuota { get; set; }
    }

    public class EmailQuotaProfile : Profile
    {
        public EmailQuotaProfile()
        {
            CreateMap<EmailQuotaDto, EmailQuotaTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailQuotaTable>, EmailResponseDto<EmailQuotaDto>>();
            CreateMap<EmailResponseDto<List<EmailQuotaTable>>, EmailResponseDto<List<EmailQuotaDto>>>();

        }
    }
}
