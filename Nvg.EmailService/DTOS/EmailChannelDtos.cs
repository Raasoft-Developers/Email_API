using AutoMapper;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.DTOS
{
    public class EmailChannelDto
    {
        public string ID { get; set; }
        public string Key { get; set; }
        public string EmailPoolID { get; set; }
        public string EmailPoolName { get; set; }
        public string EmailProviderID { get; set; }
        public string EmailProviderName { get; set; }
    }

    public class EmailChannelProfile : Profile
    {
        public EmailChannelProfile()
        {
            CreateMap<EmailChannelDto, EmailChannelTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailChannelTable>, EmailResponseDto<EmailChannelDto>>();
        }
    }
}
