using AutoMapper;
using EmailService.Data;
using EmailService.Data.EmailChannel;
using EmailService.Data.EmailErrorLog;
using EmailService.Data.EmailHistory;
using EmailService.Data.EmailPool;
using EmailService.Data.EmailProvider;
using EmailService.Data.EmailQuota;
using EmailService.Data.EmailTemplate;
using EmailService.Data.Entities;
using EmailService.DTOS;
using EmailService.Email;
using EmailService.EmailChannel;
using EmailService.EmailErrorLog;
using EmailService.EmailHistory;
using EmailService.EmailPool;
using EmailService.EmailProvider;
using EmailService.EmailQuota;
using EmailService.EmailTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmailService
{
    public static class EmailServiceExtension
    {
        public static void AddEmailServices(this IServiceCollection services, string microservice, string databaseProvider)
        {
            services.AddScoped<IEmailEventInteractor, EmailEventInteractor>();
            services.AddScoped<IEmailManagementInteractor, EmailManagementInteractor>();
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

            services.AddScoped<IEmailErrorLogRepository, EmailErrorLogRepository>();
            services.AddScoped<IEmailErrorLogInteractor, EmailErrorLogInteractor>();

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
                default:
                    services.AddScoped<EmailDbContext, EmailPgSqlDBContext>(provider =>
                    {
                        var dbInfo = provider.GetService<EmailDBInfo>();
                        var builder = new DbContextOptionsBuilder<EmailPgSqlDBContext>();
                        builder.UseNpgsql(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));

                        return new EmailPgSqlDBContext(builder.Options, microservice);
                    });
                    break;
            }
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var emailConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmailHistoryProfile>();
                cfg.CreateMap<EmailHistoryTable, EmailHistoryDto>();
            });
            IMapper notificationMapper = new Mapper(emailConfig);
            notificationMapper.Map<EmailHistoryTable, EmailHistoryDto>(new EmailHistoryTable());
        }

    }
}
