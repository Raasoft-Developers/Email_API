using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.Data.Entities
{
    [Table("EmailPool")]
    public class EmailPoolTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
