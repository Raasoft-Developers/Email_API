using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailService.Data
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly EmailDbContext _context;
        private readonly ILogger<EmailTemplateRepository> _logger;

        public EmailTemplateRepository(EmailDbContext context, ILogger<EmailTemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public EmailTemplateModel GetEmailTemplate(long templateID)
        {
            return _context.EmailTemplate.FirstOrDefault(x => x.ID == templateID);
        }

        public EmailTemplateModel GetEmailTemplate(string templateName, string tenantID = null, string facilityID = null)
        {
            string defaultTemplate = "DEFAULT_EMAIL_NOTIFICATION";
            var qry = from st in _context.EmailTemplate
                      where st.Name == templateName
                      && (st.TenantID == null && st.FacilityID == null ||
                          tenantID != null && st.TenantID == tenantID ||
                          facilityID != null && st.FacilityID == facilityID)
                      select st;

            var emailTemplate = qry.FirstOrDefault();

            if(emailTemplate == null)
                emailTemplate = _context.EmailTemplate.FirstOrDefault(t => t.Name == defaultTemplate);

            _logger.LogDebug($"Template used : {emailTemplate.Name}");

            return emailTemplate;
        }

    }
}
