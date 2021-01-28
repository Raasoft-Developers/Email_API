using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using Nvg.EmailService.Data.Entities;

namespace Nvg.EmailService.Data
{
    public class EmailDbContextSeed
    {
        public async Task SeedAsync(EmailDbContext context, IConfiguration configuration, dynamic emailTemplatesData)
        {
            await context.Database.MigrateAsync();

            if (context.EmailTemplate.Any())
            {
                foreach (var emailTemplate in emailTemplatesData)
                {
                    var name = (string)emailTemplate.name;
                    var emailBodyTemplate = (string)emailTemplate.emailBodyTemplate;
                    var subjectTemplate = (string)emailTemplate.subjectTemplate;

                    var emailTemplateFromTblHasValue = context.EmailTemplate.Any(e => e.Name == name && e.SubjectTemplate == subjectTemplate && e.EmailBodyTemplate == emailBodyTemplate);
                    if (!emailTemplateFromTblHasValue)
                        SeedEmailTemplate(context, emailTemplate);
                }
            }
            else
            {
                foreach (var emailTemplate in emailTemplatesData)
                    SeedEmailTemplate(context, emailTemplate);
            }
        }

        private static void SeedEmailTemplate(EmailDbContext context, dynamic emailTemplate)
        {
            context.EmailTemplate.Add(new EmailTemplateModel
            {
                Name = emailTemplate.name,
                EmailBodyTemplate = emailTemplate.emailBodyTemplate,
                SubjectTemplate = emailTemplate.subjectTemplate
            });
            context.SaveChanges();
        }
    }
}
