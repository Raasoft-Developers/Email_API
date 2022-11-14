using AutoMapper;
using EmailService.Data.Entities;
using System;
using System.Collections.Generic;

namespace EmailService.DTOS
{
    public class EmailHistoryDto
    {
        public string ID { get; set; }
        public string MessageSent { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public DateTime SentOn { get; set; }
        public string TemplateName { get; set; }
        public string TemplateVariant { get; set; }
        public string EmailChannelID { get; set; }
        public string ChannelKey { get; set; }
        public string EmailProviderID { get; set; }
        public string ProviderName { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
    }

    public class EmailHistoryProfile : Profile
    {
        public EmailHistoryProfile()
        {
            CreateMap<EmailHistoryDto, EmailHistoryTable>().ReverseMap();
            CreateMap<EmailResponseDto<EmailHistoryTable>, EmailResponseDto<EmailHistoryDto>>();
            CreateMap<EmailResponseDto<List<EmailHistoryTable>>, EmailResponseDto<List<EmailHistoryDto>>>();
        }
    }
}
