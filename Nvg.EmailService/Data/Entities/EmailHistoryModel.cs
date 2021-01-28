using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailHistory")]
    public class EmailHistoryModel
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string ToEmailID { get; set; }
        public string MailBody { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SentOn { get; set; }
        public string Status { get; set; }
    }
}
