using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailPool")]
    public class EmailPoolTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
