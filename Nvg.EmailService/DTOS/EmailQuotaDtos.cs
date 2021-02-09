using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.DTOS
{
    public class EmailQuotaDto
    {
        public string ID { get; set; }
        public string ChannelID { get; set; }
        public int TotalConsumption { get; set; }
        public int MonthylConsumption { get; set; }
        public int MonthlyQuota { get; set; }
    }

    public class EmailQuotaProfile : Profile
    {
        public EmailQuotaProfile()
        {
            CreateMap<EmailQuotaDto, EmailQuotaTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailQuotaTable>, EmailResponseDto<EmailQuotaDto>>();

        }
    }
}
