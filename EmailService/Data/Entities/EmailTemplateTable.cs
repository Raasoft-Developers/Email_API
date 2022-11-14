using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Data.Entities
{
    [Table("EmailTemplate")]
    public class EmailTemplateTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string EmailPoolID { get; set; }
        [ForeignKey("EmailPoolID")]
        public EmailPoolTable EmailPool { get; set; }
        public string MessageTemplate { get; set; }
    }
}
