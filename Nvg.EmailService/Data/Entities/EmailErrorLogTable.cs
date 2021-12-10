using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailErrorLogTable")]

    public class EmailErrorLogTable
    {

        [Key]
        public int ID { get; set; }
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string StackTrace { get; set; }
        public string Recipients { get; set; }
        public string Subject { get; set; }
        public DateTime CreationTime { get; set; }
        public string EmailChannelID { get; set; }
        [NotMapped]
        public string ChannelKey { get; set; }
        [ForeignKey("EmailChannelID")]
        public EmailChannelTable EmailChannel { get; set; }

    }
}
