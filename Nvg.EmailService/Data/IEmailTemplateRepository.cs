using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailService.Data
{
    public interface IEmailTemplateRepository
    {
        EmailTemplateModel GetEmailTemplate(long templateID);
        EmailTemplateModel GetEmailTemplate(string templateName, string tenantID = null, string facilityID = null);
    }
}
