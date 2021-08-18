using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailQuota")]
    public class EmailQuotaTable
    {
        [Key]
        public long ID { get; set; }
        public string EmailChannelID { get; set; }
        [ForeignKey("EmailChannelID")]
        public EmailChannelTable EmailChannel { get; set; }
        public int TotalConsumption { get; set; }
        public int MonthlyConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public int MonthlyQuota { get; set; }
        public int TotalQuota { get; set; }
        [NotMapped]
        public string EmailChannelKey { get; set; }
    }
}
