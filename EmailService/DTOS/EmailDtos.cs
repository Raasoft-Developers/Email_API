using AutoMapper;
using System.Collections.Generic;

namespace EmailService.DTOS
{
    public class EmailDto
    {
        public string ChannelKey { get; set; }
        public string TemplateName { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public List<EmailAttachment> Files { get; set; }
        public string Tag { get; set; }
    }
    public class EmailBalanceDto
    {
        public bool HasLimit { get; set; }
        public bool IsExceeded { get; set; }
        public int Balance { get; set; }
    }
    public class EmailAttachment
    {
        public string FileContent { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

    public class EmailResponseDto<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }


    public class EmailDTOProfile : Profile
    {
        public EmailDTOProfile()
        {

        }
    }

}
