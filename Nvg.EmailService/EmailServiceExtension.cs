using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nvg.EmailService.Data;
using Nvg.EmailService.Data.EmailChannel;
using Nvg.EmailService.Data.EmailHistory;
using Nvg.EmailService.Data.EmailPool;
using Nvg.EmailService.Data.EmailProvider;
using Nvg.EmailService.Data.EmailQuota;
using Nvg.EmailService.Data.EmailTemplate;
using Nvg.EmailService.Email;
using Nvg.EmailService.EmailChannel;
using Nvg.EmailService.EmailHistory;
using Nvg.EmailService.EmailPool;
using Nvg.EmailService.EmailProvider;
using Nvg.EmailService.EmailQuota;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nvg.EmailService
{
    public static class EmailServiceExtension
    {
        public static void AddEmailServices(this IServiceCollection services, string microservice, string databaseProvider)
        {
            services.AddScoped<IEmailEventInteractor, EmailEventInteractor>();

            services.AddScoped<IEmailInteractor, EmailInteractor>();

            services.AddScoped<IEmailChannelRepository, EmailChannelRepository>();
            services.AddScoped<IEmailChannelInteractor, EmailChannelInteractor>();

            services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();
            services.AddScoped<IEmailHistoryInteractor, EmailHistoryInteractor>();

            services.AddScoped<IEmailPoolRepository, EmailPoolRepository>();
            services.AddScoped<IEmailPoolInteractor, EmailPoolInteractor>();

            services.AddScoped<IEmailProviderRepository, EmailProviderRepository>();
            services.AddScoped<IEmailProviderInteractor, EmailProviderInteractor>();

            services.AddScoped<IEmailQuotaRepository, EmailQuotaRepository>();
            services.AddScoped<IEmailQuotaInteractor, EmailQuotaInteractor>();

            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailTemplateInteractor, EmailTemplateInteractor>();
            databaseProvider ??= string.Empty;
            switch (databaseProvider.ToLower())
            {
                case "postgresql":
                    services.AddScoped<EmailDbContext, EmailPgSqlDBContext>(provider =>
                    {
                        var dbInfo = provider.GetService<EmailDBInfo>();
                        var builder = new DbContextOptionsBuilder<EmailPgSqlDBContext>();
                        builder.UseNpgsql(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));

                        return new EmailPgSqlDBContext(builder.Options, microservice);
                    });
                    break;
                case "mssql":
                    services.AddScoped<EmailDbContext, EmailSqlServerDBContext>(provider =>
                    {
                        var dbInfo = provider.GetService<EmailDBInfo>();
                        var builder = new DbContextOptionsBuilder<EmailSqlServerDBContext>();
                        builder.UseSqlServer(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));

                        return new EmailSqlServerDBContext(builder.Options, microservice);
                    });
                    break;
                case "oracle":
                    services.AddScoped<EmailDbContext, EmailOracleDBContext>(provider =>
                    {
                        var dbInfo = provider.GetService<EmailDBInfo>();
                        var builder = new DbContextOptionsBuilder<EmailOracleDBContext>();
                        builder.UseOracle(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));

                        return new EmailOracleDBContext(builder.Options, microservice);
                    });
                    break;
                default:
                    services.AddScoped<EmailDbContext, EmailSqlServerDBContext>(provider =>
                    {
                        var dbInfo = provider.GetService<EmailDBInfo>();
                        var builder = new DbContextOptionsBuilder<EmailSqlServerDBContext>();
                        builder.UseSqlServer(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));

                        return new EmailSqlServerDBContext(builder.Options, microservice);
                    });
                    break;
            }
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
