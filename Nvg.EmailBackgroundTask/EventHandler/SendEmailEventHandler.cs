using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Nvg.EmailBackgroundTask.Extensions;
using Nvg.EmailBackgroundTask.EmailProvider;
using Nvg.EmailBackgroundTask.Events;
using Nvg.EmailService;
using Nvg.EmailService.Data;
using Nvg.EmailBackgroundTask.Models;

namespace Nvg.EmailBackgroundTask.EventHandler
{
    public class SendEmailEventHandler : IIntegrationEventHandler<SendEmailEvent>
    {
        private readonly ILogger<SendEmailEventHandler> _logger;

        public SendEmailEventHandler(ILogger<SendEmailEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task<dynamic> Handle(SendEmailEvent @event)
        {
            _logger.LogDebug($"Subscriber received a SendEmailEvent notification.");
            if (@event.Id != Guid.Empty)
            {
                using IServiceScope scope = GetScope(@event.TenantID, @event.FacilityID);
                var emailManager = scope.ServiceProvider.GetService<EmailManager>();

                var emailCounterInteractor = scope.ServiceProvider.GetService<IEmailCounterInteractor>();
                var emailCount = emailCounterInteractor.GetEmailCounter(@event.TenantID, @event.FacilityID);

                //var emailSettings = scope.ServiceProvider.GetService<EmailSettings>();
                var emailSettings = scope.ServiceProvider.GetService<EmailProviderConnectionString>();
                _logger.LogDebug($"Enable Email Info: {emailSettings}");

                /*if (emailSettings.EnableEmail)
                {*/
                var email = new Email
                {
                    FacilityID = @event.FacilityID,
                    EmailParts = @event.EmailParts,
                    Subject = @event.SubjectParts,
                    Sender = emailSettings.Sender,
                    TemplateName = @event.TemplateName,
                    TenantID = @event.TenantID,
                    To = @event.Recipients
                };
                emailManager.SendEmail(email);
                /*}
                else
                    _logger.LogDebug($"Email settings are not Enabled or you have crossed the MonthlyEmailQuota");
                */
            }
            return Task.FromResult(true);
        }

        private IServiceScope GetScope(string tenantID, string facilityID)
        {
            _logger.LogDebug($"GetScope() : TENANTID - {tenantID}, FACILITYID - {facilityID}");

            var serviceProvider = new ServiceCollection();

            var configuration = Program.GetConfiguration();
            serviceProvider.AddScoped(_ => configuration);
            serviceProvider.AddLogging();
            serviceProvider.AddEmailBackgroundTask();
            serviceProvider.AddEmailServices(Program.AppName);
            serviceProvider.AddHttpContextAccessor();

            serviceProvider.AddScoped<EmailDBInfo>(provider =>
            {
                string microservice = Program.AppName;
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                return new EmailDBInfo(connectionString);
            });

            serviceProvider.AddScoped<EmailProviderConnectionString>(provider =>
            {
                string emailGatewayProvider = configuration.GetSection("EmailGatewayProvider")?.Value;
                _logger.LogDebug($"EmailGatewayProvider : {emailGatewayProvider}");
                return new EmailProviderConnectionString(emailGatewayProvider);
            });
            
            var scope = serviceProvider.BuildServiceProvider().CreateScope();
            return scope;
        }
    }
}
