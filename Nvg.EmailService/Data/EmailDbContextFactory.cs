using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailDbContextFactory : IDesignTimeDbContextFactory<EmailDbContext>
    {
        public EmailDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmailDbContext>();
            // For Removing migration or Updating the database, uncomment the hardcoded connection string.
            //string connectionString = "host=db-postgresql-blr1-95081-do-user-7578230-0.a.db.ondigitalocean.com;Database=tv_etms;User ID =nyletech;Password=Nyle@123;Port=25061;Integrated Security=true;Pooling=true;sslmode=Require;Trust Server Certificate=true;Server Compatibility Mode=Redshift;";
            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseNpgsql("Email-ConnectionString");
            return new EmailDbContext(optionsBuilder.Options, "Email"); // TODO: Should avoid hardcoding of schema.
        }
    }
}
