using Microsoft.EntityFrameworkCore;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailDbContext : DbContext
    {
        public virtual DbSet<EmailChannelTable> EmailChannels { get; set; }       
        public virtual DbSet<EmailHistoryTable> EmailHistories { get; set; }
        public virtual DbSet<EmailPoolTable> EmailPools { get; set; }
        public virtual DbSet<EmailProviderSettingsTable> EmailProviders { get; set; }
        public virtual DbSet<EmailQuotaTable> EmailQuotas { get; set; }
        public virtual DbSet<EmailTemplateTable> EmailTemplates { get; set; }

        public string _schema { get; set; }

        public EmailDbContext(DbContextOptions options) : base(options)
        {

        }

        public EmailDbContext(DbContextOptions options, string schema) : base(options)
        {
            _schema = schema;
        }

        
    }
}
