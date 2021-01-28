using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nvg.EmailService.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nvg.EmailService
{
    public static class EmailServiceExtension
    {
        public static void AddEmailServices(this IServiceCollection services, string microservice)
        {
            services.AddScoped<IEmailInteractor, EmailInteractor>();
            services.AddScoped<IEmailCounterInteractor, EmailCounterInteractor>();
            services.AddScoped<IEmailCounterRepository, EmailCounterRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailTemplateInteractor, EmailTemplateInteractor>();
            services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();
            services.AddScoped<IEmailHistoryInteractor, EmailHistoryInteractor>();

            services.AddScoped<EmailDbContext>(provider =>
            {
                var dbInfo = provider.GetService<EmailDBInfo>();
                var builder = new DbContextOptionsBuilder<EmailDbContext>();
                builder.UseNpgsql(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                return new EmailDbContext(builder.Options, microservice);
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
