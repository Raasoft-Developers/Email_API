using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Data.Entities
{
    [Table("EmailHistory")]
    public class EmailHistoryTable
    {
        [Key]
        public string ID { get; set; }
        public string MessageSent { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public DateTime SentOn { get; set; }
        public string TemplateName { get; set; }
        public string TemplateVariant { get; set; }
        public string EmailChannelID { get; set; }
        [NotMapped]
        public string ChannelKey { get; set; }
        [ForeignKey("EmailChannelID")]
        public EmailChannelTable EmailChannel { get; set; }
        public string EmailProviderID { get; set; }
        [NotMapped]
        public string ProviderName { get; set; }
        [ForeignKey("EmailProviderID")]
        public EmailProviderSettingsTable EmailProvider { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
    }
}
