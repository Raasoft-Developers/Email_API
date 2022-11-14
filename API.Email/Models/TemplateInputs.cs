using AutoMapper;
using EmailService.DTOS;

namespace API.Email.Models
{
    public class TemplateInput
    {
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string EmailPoolID { get; set; }
        public string EmailPoolName { get; set; }
        public string MessageTemplate { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class TemplateMgmtInput : TemplateInput
    {
        public string ID { get; set; }
    }

    public class EmailTemplateInputProfile : Profile
    {
        public EmailTemplateInputProfile()
        {
            CreateMap<TemplateInput, EmailTemplateDto>().ReverseMap();
            CreateMap<TemplateMgmtInput, EmailTemplateDto>().ReverseMap();
        }
    }
}
