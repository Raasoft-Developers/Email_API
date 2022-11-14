using EmailService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Data
{
    public class EmailPgSqlDBContext : EmailDbContext
    {
        public EmailPgSqlDBContext(DbContextOptions<EmailPgSqlDBContext> options) : base(options)
        {
        }

        public EmailPgSqlDBContext(DbContextOptions<EmailPgSqlDBContext> options, string schema) : base(options)
        {
            _schema = schema;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<EmailPoolTable>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            modelBuilder.Entity<EmailChannelTable>()
              .HasIndex(x => x.Key).IsUnique(true);

            modelBuilder.Entity<EmailProviderSettingsTable>()
               .HasIndex(p => new { p.Name, p.EmailPoolID })
               .IsUnique(true);

            modelBuilder.Entity<EmailTemplateTable>()
               .HasIndex(p => new { p.Name, p.EmailPoolID })
               .IsUnique(true);
        }

    }
}
