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
        /// <summary>
        /// Method to seed data into the database tables.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        /// <param name="emailChannelData">Dynamic Data</param>
        /// <param name="emailPoolData">Dynamic Data</param>
        /// <param name="emailProviderData">Dynamic Data</param>
        /// <param name="emailTemplatesData">Dynamic Data</param>
        /// <returns><see cref="Task"/></returns>
        public async Task SeedAsync(EmailDbContext context, IConfiguration configuration, dynamic emailChannelData, dynamic emailPoolData, 
            dynamic emailProviderData, dynamic emailTemplatesData)
        {
            await context.Database.MigrateAsync();
            MigrateEmailPoolData(context,emailPoolData);
            MigrateEmailProviderData(context, emailProviderData);
            MigrateEmailChannelData(context, emailChannelData);
            MigrateEmailTemplateData(context,emailTemplatesData);
        }

        #region Migrate methods
        /// <summary>
        /// Migrate data into Email Template table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailTemplatesData">Dynamic data</param>
        private void MigrateEmailTemplateData(EmailDbContext context, dynamic emailTemplatesData)
        {
            if (emailTemplatesData != null)
            {
                if (context.EmailTemplates.Any())
                {
                    foreach (var emailTemplateRow in emailTemplatesData)
                    {
                        var name = (string)emailTemplateRow.name;
                        var messageTemplate = (string)emailTemplateRow.messageTemplate;
                        var emailPoolId = (string)emailTemplateRow.emailPoolID;

                        var emailTemplateFromTblHasValue = context.EmailTemplates.Any(e => e.Name == name && e.MessageTemplate == messageTemplate && e.EmailPoolID == emailPoolId);
                        if (!emailTemplateFromTblHasValue)
                            SeedEmailTemplate(context, emailTemplateRow);
                    }
                }
                else
                {
                    foreach (var emailTemplate in emailTemplatesData)
                        SeedEmailTemplate(context, emailTemplate);
                }
            }
        }

        /// <summary>
        /// Migrate data into Email Pool table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailPoolData">Dynamic data</param>
        private void MigrateEmailPoolData(EmailDbContext context, dynamic emailPoolData)
        {
            if(emailPoolData != null)
            {
                if (context.EmailPools.Any())
                {
                    foreach(var poolRow in emailPoolData)
                    {
                        var name = (string)poolRow.name;
                        var emailPoolFromTblHasValue = context.EmailPools.Any(e => e.Name == name);
                        if (!emailPoolFromTblHasValue)
                            SeedEmailPool(context,poolRow);
                    }
                }
                else
                {
                    foreach (var pool in emailPoolData)
                        SeedEmailPool(context,pool);
                }
            }
        }

        /// <summary>
        /// Migrate data into Email Channel table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailChannelData">Dynamic data</param>
        private void MigrateEmailChannelData(EmailDbContext context, dynamic emailChannelData)
        {
            if(emailChannelData != null)
            {
                if (context.EmailChannels.Any())
                {
                    foreach(var channelRow in emailChannelData)
                    {
                        var key = (string)channelRow.key;
                        var emailPoolId = (string)channelRow.emailPoolID;
                        var emailChannelFromTblHasValue = context.EmailChannels.Any(e => e.Key == key && e.EmailPoolID == emailPoolId);
                        if (!emailChannelFromTblHasValue)
                            SeedEmailChannel(context,channelRow);
                    }
                }
                else
                {
                    foreach(var channelRow in emailChannelData)
                        SeedEmailChannel(context, channelRow);
                }
            }
        }

        /// <summary>
        /// Migrate data into Email Provider Settings table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailProviderData">Dynamic data</param>
        private void MigrateEmailProviderData(EmailDbContext context, dynamic emailProviderData)
        {
            if(emailProviderData != null)
            {
                if (context.EmailProviders.Any())
                {
                    foreach(var providerRow in emailProviderData)
                    {
                        var name = (string)providerRow.name;
                        var configuration = (string)providerRow.configuration;
                        var emailPoolId = (string)providerRow.emailPoolID;
                        var emailProviderFromTblHasValue = context.EmailProviders.Any(e => e.Name == name && e.Configuration == configuration && e.EmailPoolID == emailPoolId);
                        if (!emailProviderFromTblHasValue)
                            SeedEmailProvider(context, providerRow);
                    }
                }
                else
                {
                    foreach (var providerRow in emailProviderData)
                        SeedEmailProvider(context, providerRow);
                }
            }
        }
        #endregion

        #region Seed methods
        /// <summary>
        /// Insert Email template data into table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailTemplate">Dynamic row data</param>
        private static void SeedEmailTemplate(EmailDbContext context, dynamic emailTemplate)
        {
            context.EmailTemplates.Add(new EmailTemplateTable
            {
                ID = emailTemplate.id,
                Name = emailTemplate.name,
                EmailPoolID = emailTemplate.emailPoolID,
                MessageTemplate = emailTemplate.messageTemplate,
                Variant = emailTemplate.variant,
                Sender = emailTemplate.sender
            });
            context.SaveChanges();
        }

        /// <summary>
        /// Insert Email Pool table into table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailPool">Dynamic row data</param>
        private static void SeedEmailPool(EmailDbContext context, dynamic emailPool)
        {
            context.EmailPools.Add(new EmailPoolTable
            {
                ID = emailPool.id,
                Name = emailPool.name
            });
            context.SaveChanges();
        }

        /// <summary>
        /// Insert Email Channel data into table.
        /// </summary>
        /// <param name="context"><see cref="EmailDbContext"/></param>
        /// <param name="emailChannel">Dynamic row data</param>
        private static void SeedEmailChannel(EmailDbContext context, dynamic emailChannel)
        {
            context.EmailChannels.Add(new EmailChannelTable
            {
                ID = emailChannel.id,
                Key = emailChannel.key,
                EmailPoolID = emailChannel.emailPoolID,
                EmailProviderID = emailChannel.emailProviderID

            });
            context.SaveChanges();
        }

        /// <summary>
        /// Insert Email Provider Settings data into table.
        /// </summary>
        /// <param name="context"><<see cref="EmailDbContext"/>/param>
        /// <param name="emailProvider">Dynamic row data</param>
        private static void SeedEmailProvider(EmailDbContext context, dynamic emailProvider)
        {
            context.EmailProviders.Add(new EmailProviderSettingsTable
            {
                ID = emailProvider.id,
                Type = emailProvider.type,
                Configuration = emailProvider.configuration,
                EmailPoolID = emailProvider.emailPoolID,
                Name = emailProvider.name
            });
            context.SaveChanges();
        }
        #endregion
    }
}
