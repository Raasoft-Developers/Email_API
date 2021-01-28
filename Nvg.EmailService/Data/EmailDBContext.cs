using Microsoft.EntityFrameworkCore;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailDbContext : DbContext
    {
        public virtual DbSet<EmailTemplateModel> EmailTemplate { get; set; }
        public virtual DbSet<EmailHistoryModel> EmailHistory { get; set; }
        public virtual DbSet<EmailCounterModel> EmailCounter { get; set; }

        public string _schema { get; set; }

        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {

        }

        public EmailDbContext(DbContextOptions<EmailDbContext> options, string schema) : base(options)
        {
            _schema = schema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);
        }
    }
}
