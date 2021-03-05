using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    // <summary>
    // This Class file is used to generate the migrations. If Additional providers are to be added then add them below.
    // </summary>
    public class EmailPgSqlDbContextFactory : IDesignTimeDbContextFactory<EmailPgSqlDBContext>
    {
        public EmailPgSqlDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmailPgSqlDBContext>();
            // For Removing migration or Updating the database, uncomment the hardcoded connection string.
            //string connectionString = "host=db-postgresql-blr1-95081-do-user-7578230-0.a.db.ondigitalocean.com;Database=tv_etms;User ID =nyletech;Password=Nyle@123;Port=25061;Integrated Security=true;Pooling=true;sslmode=Require;Trust Server Certificate=true;Server Compatibility Mode=Redshift;";
            //optionsBuilder.UseNpgsql(connectionString);

            optionsBuilder.UseNpgsql("Email-ConnectionString");
            return new EmailPgSqlDBContext(optionsBuilder.Options,"Email"); // TODO: Should avoid hardcoding of schema.
        }
    }

    public class EmailSqlServerDbContextFactory : IDesignTimeDbContextFactory<EmailSqlServerDBContext>
    {
        public EmailSqlServerDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmailSqlServerDBContext>();
            // For Removing migration or Updating the database, uncomment line number 33 and 34 and comment 36.
            //string connectionString = "host=db-postgresql-blr1-95081-do-user-7578230-0.a.db.ondigitalocean.com;Database=tv_etms;User ID =nyletech;Password=Nyle@123;Port=25061;Integrated Security=true;Pooling=true;sslmode=Require;Trust Server Certificate=true;Server Compatibility Mode=Redshift;";
            //optionsBuilder.UseNpgsql(connectionString);

            // Comment line number 33 and 34 and uncomment 36 before creating new migration
            optionsBuilder.UseSqlServer("Email-ConnectionString");
            // Currently schema parameter will not be used while generating migrations for SQL as line number 22 is commented in EmailSqlServerDBContext.cs
            return new EmailSqlServerDBContext(optionsBuilder.Options, "dbo"); // TODO: Should avoid hardcoding of schema.
        }
    }

    //public class EmailOracleDbContextFactory : IDesignTimeDbContextFactory<EmailOracleDBContext>
    //{
    //    public EmailOracleDBContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<EmailOracleDBContext>();
    //        // For Removing migration or Updating the database, uncomment line number 49 and 50 and comment 53.
    //        //string connectionString = "host=db-postgresql-blr1-95081-do-user-7578230-0.a.db.ondigitalocean.com;Database=tv_etms;User ID =nyletech;Password=Nyle@123;Port=25061;Integrated Security=true;Pooling=true;sslmode=Require;Trust Server Certificate=true;Server Compatibility Mode=Redshift;";
    //        //optionsBuilder.UseNpgsql(connectionString);

    //        // Comment line number 49 and 50 and uncomment 53 before creating new migration
    //        optionsBuilder.UseOracle("Email-ConnectionString");
    //        // Currently schema parameter will not be used while generating migrations for SQL as line number 21 is commented in EmailOracleDBContext.cs
    //        return new EmailOracleDBContext(optionsBuilder.Options, ""); // TODO: Should avoid hardcoding of schema.
    //    }
    //}
}
