using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Dtos
{
    public class EmailDto
    {
        public string To { get; set; }
        public string ResetPasswordLink { get; set; }
        public string Subject { get; set; }
        public string TenantID { get; set; }
    }

    public class EmailTemplateDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Name { get; set; }
        public string EmailBodyTemplate { get; set; }
        public string SubjectTemplate { get; set; }
    }

    public class EmailHistoryDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string ToEmailID { get; set; }
        public string MailBody { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SentOn { get; set; }
        public string Status { get; set; }
    }

    public class EmailCounterDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public int Count { get; set; }
        public string FacilityID { get; set; }
    }


    public class EmailDTOProfile : Profile
    {
        public EmailDTOProfile()
        {
            CreateMap<EmailTemplateModel, EmailTemplateDto>().ReverseMap();
            CreateMap<EmailHistoryModel, EmailHistoryDto>().ReverseMap();
            CreateMap<EmailCounterDto, EmailCounterModel>().ReverseMap();
        }
    }

}
