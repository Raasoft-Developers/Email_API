﻿using Microsoft.EntityFrameworkCore;
using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailSqlServerDBContext : EmailDbContext
    {
        public EmailSqlServerDBContext(DbContextOptions<EmailSqlServerDBContext> options) : base(options)
        {
        }

        public EmailSqlServerDBContext(DbContextOptions<EmailSqlServerDBContext> options, string schema) : base(options)
        {
            _schema = schema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<EmailPoolTable>()
                .HasIndex(p => p.Name)
                .IsUnique(true);
        }
    }
}
