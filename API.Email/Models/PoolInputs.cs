using AutoMapper;
using EmailService.DTOS;

namespace API.Email.Models
{
    public class PoolInput
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class PoolMgmtInput : PoolInput
    {
        public string ID { get; set; }
    }

    public class PoolInputProfile : Profile
    {
        public PoolInputProfile()
        {
            CreateMap<PoolInput, EmailPoolDto>().ReverseMap();
            CreateMap<PoolMgmtInput, EmailPoolDto>().ReverseMap();
        }
    }
}
