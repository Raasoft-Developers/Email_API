using AutoMapper;
using Nvg.EmailService.DTOS;

namespace Nvg.Api.Email.Models
{
    public class ChannelInput
    {
        public string Key { get; set; }
        public string EmailPoolID { get; set; }
        public string EmailPoolName { get; set; }
        public string EmailProviderID { get; set; }
        public string EmailProviderName { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class ChannelMgmtInput : ChannelInput
    {
        public string ID { get; set; }
    }

    public class EmailChannelInputProfile : Profile
    {
        public EmailChannelInputProfile()
        {
            CreateMap<ChannelInput, EmailChannelDto>().ReverseMap();
            CreateMap<ChannelMgmtInput, EmailChannelDto>().ReverseMap();
        }
    }
}
