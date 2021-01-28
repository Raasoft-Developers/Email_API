using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    [Table("EmailTemplate")]
    public class EmailTemplateModel
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Name { get; set; }
        public string EmailBodyTemplate { get; set; }
        public string SubjectTemplate { get; set; }
    }
}
