﻿using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System.Collections.Generic;

namespace Nvg.EmailService.DTOS
{
    public class EmailPoolDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class EmailPoolProfile : Profile
    {
        public EmailPoolProfile()
        {
            CreateMap<EmailPoolDto, EmailPoolTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailPoolTable>, EmailResponseDto<EmailPoolDto>>();
            CreateMap<EmailResponseDto<List<EmailPoolTable>>, EmailResponseDto<List<EmailPoolDto>>>();

        }
    }

}
