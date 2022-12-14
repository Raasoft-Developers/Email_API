using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Data.Entities
{
    [Table("EmailProviderSettings")]
    public class EmailProviderSettingsTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string EmailPoolID { get; set; }
        [ForeignKey("EmailPoolID")]
        public EmailPoolTable EmailPool { get; set; }
    }
}
