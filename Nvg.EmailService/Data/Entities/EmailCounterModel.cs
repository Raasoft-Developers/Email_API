using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.EmailService.Data.Entities
{
    public class EmailCounterModel
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string Count { get; set; }
        public string FacilityID { get; set; }
    }
}
