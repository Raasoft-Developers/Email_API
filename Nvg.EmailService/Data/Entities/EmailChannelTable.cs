﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailChannel")]
    public class EmailChannelTable
    {
        [Key]
        public string ID { get; set; }
        public string Key { get; set; }

        public string EmailPoolID { get; set; }
        [ForeignKey("EmailPoolID")]
        public EmailPoolTable EmailPool { get; set; }

        public string EmailProviderID { get; set; }
        [ForeignKey("EmailProviderID")]
        public EmailProviderSettingsTable EmailProvider { get; set; }
    }
}
