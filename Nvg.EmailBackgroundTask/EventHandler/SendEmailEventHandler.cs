﻿using System;
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
using Nvg.EmailService.EmailQuota;
using Nvg.EmailService.EmailProvider;

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
                using IServiceScope scope = GetScope(@event.ChannelKey);
                var emailManager = scope.ServiceProvider.GetService<EmailManager>();

                //var emailQuotaInteractor = scope.ServiceProvider.GetService<IEmailQuotaInteractor>();
                //var emailQuota = emailQuotaInteractor.GetEmailQuota(@event.ChannelKey)?.Result;

                var emailProviderService = scope.ServiceProvider.GetService<IEmailProviderInteractor>();
                var providerName = emailProviderService.GetEmailProviderByChannel(@event.ChannelKey)?.Result?.Name;

                /*if (emailSettings.EnableEmail)
                {*/
                var email = new Email
                {
                    Recipients = @event.Recipients,
                    Sender = @event.Sender,
                    TemplateName = @event.TemplateName,
                    Variant = @event.Variant,
                    ChannelKey = @event.ChannelKey,
                    MessageParts = @event.MessageParts,
                    ProviderName = providerName,
                    Subject= @event.Subject,
                    Tag = @event.Tag
                };
                emailManager.SendEmail(email);
                /*}
                else
                    _logger.LogDebug($"Email settings are not Enabled or you have crossed the MonthlyEmailQuota");
                */
            }
            return Task.FromResult(true);
        }

        private IServiceScope GetScope(string channelKey)
        {
            var services = new ServiceCollection();
            var configuration = Program.GetConfiguration();
            services.AddScoped(_ => configuration);
            services.AddLogging();
            services.AddEmailBackgroundTask(channelKey);
            services.AddEmailServices(Program.AppName);

            services.AddScoped<EmailDBInfo>(provider =>
            {
                string microservice = Program.AppName;
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                return new EmailDBInfo(connectionString);
            });

            services.AddScoped<EmailProviderConnectionString>(provider =>
            {
                var emailProviderService = provider.GetService<IEmailProviderInteractor>();
                var emailProviderConfiguration = emailProviderService.GetEmailProviderByChannel(channelKey)?.Result?.Configuration;

                return new EmailProviderConnectionString(emailProviderConfiguration);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            return scope;
        }
    }
}