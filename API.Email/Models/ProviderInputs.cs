using AutoMapper;
using EmailService.DTOS;

namespace API.Email.Models
{
    public class ProviderInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string EmailPoolName { get; set; }
        public string EmailPoolID { get; set; }
    }

    /// <summary>
    /// Input model for Management controller
    /// </summary>
    public class ProviderMgmtInput : ProviderInput
    {
        public string ID { get; set; }
    }

    public class EmailProviderInputProfile : Profile
    {
        public EmailProviderInputProfile()
        {
            CreateMap<ProviderInput, EmailProviderSettingsDto>().ReverseMap();
            CreateMap<ProviderMgmtInput, EmailProviderSettingsDto>().ReverseMap();
        }
    }
}
