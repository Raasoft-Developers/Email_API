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
        public static void AddEmailServices(this IServiceCollection services, string microservice)
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
