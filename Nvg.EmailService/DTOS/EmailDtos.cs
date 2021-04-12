﻿using AutoMapper;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.DTOS
{
    public class EmailDto
    {
        public string ChannelKey { get; set; }
        public string TemplateName { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public Dictionary<string,string> MessageParts { get; set; }
        public string Tag { get; set; }
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
