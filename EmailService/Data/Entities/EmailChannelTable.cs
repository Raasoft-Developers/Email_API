using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Data.Entities
{
    [Table("EmailChannel")]
    public class EmailChannelTable
    {
        [Key]
        public string ID { get; set; }
        public string Key { get; set; }

        public string EmailPoolID { get; set; }
        [NotMapped]
        public string EmailPoolName { get; set; }
        [ForeignKey("EmailPoolID")]
        public EmailPoolTable EmailPool { get; set; }

        public string EmailProviderID { get; set; }
        [NotMapped]
        public string EmailProviderName { get; set; }
        [ForeignKey("EmailProviderID")]
        public EmailProviderSettingsTable EmailProvider { get; set; }
    }
}
