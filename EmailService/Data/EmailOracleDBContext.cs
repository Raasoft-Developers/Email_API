using EmailService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Data
{
    public class EmailOracleDBContext : EmailDbContext
    {
        public EmailOracleDBContext(DbContextOptions<EmailOracleDBContext> options) : base(options)
        {
        }

        public EmailOracleDBContext(DbContextOptions<EmailOracleDBContext> options, string schema) : base(options)
        {
            _schema = schema;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<EmailPoolTable>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            modelBuilder.Entity<EmailChannelTable>()
             .HasIndex(x => x.Key).IsUnique(true);
        }
    }
}
