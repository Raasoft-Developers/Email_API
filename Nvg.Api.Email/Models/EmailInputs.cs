using AutoMapper;
using Nvg.EmailService.DTOS;
using System.Collections.Generic;

namespace Nvg.Api.Email.Models
{
    public class EmailInput
    {
        public string ChannelKey { get; set; }
        public string TemplateName { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Tag { get; set; }
    }

    public class EmailInputProfile : Profile
    {
        public EmailInputProfile()
        {
            CreateMap<EmailInput, EmailDto>().ReverseMap();
        }
    }
}
