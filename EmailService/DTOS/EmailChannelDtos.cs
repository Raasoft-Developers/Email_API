using AutoMapper;
using EmailService.Data.Entities;
using System.Collections.Generic;

namespace EmailService.DTOS
{
    public class EmailChannelDto
    {
        public string ID { get; set; }
        public string Key { get; set; }
        public string EmailPoolID { get; set; }
        public string EmailPoolName { get; set; }
        public string EmailProviderID { get; set; }
        public string EmailProviderName { get; set; }
        public int MonthlyQuota { get; set; }
        public int TotalQuota { get; set; }
        public int MonthlyConsumption { get; set; }
        public int TotalConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public bool IsRestrictedByQuota { get; set; }
    }

    public class EmailChannelProfile : Profile
    {
        public EmailChannelProfile()
        {
            CreateMap<EmailChannelDto, EmailChannelTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailChannelTable>, EmailResponseDto<EmailChannelDto>>();
            CreateMap<EmailResponseDto<List<EmailChannelTable>>, EmailResponseDto<List<EmailChannelDto>>>();
        }
    }
}
