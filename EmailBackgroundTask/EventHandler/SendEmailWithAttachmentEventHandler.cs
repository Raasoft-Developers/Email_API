using EmailBackgroundTask.Events;
using EmailBackgroundTask.Extensions;
using EmailBackgroundTask.Models;
using EmailService;
using EmailService.Data;
using EmailService.EmailProvider;
using EmailService.EmailServiceProviders;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmailBackgroundTask.EventHandler
{
    public class SendEmailWithAttachmentEventHandler : IIntegrationEventHandler<SendEmailWithAttachmentEvent>
    {
        private readonly ILogger<SendEmailWithAttachmentEventHandler> _logger;

        public SendEmailWithAttachmentEventHandler(ILogger<SendEmailWithAttachmentEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task<dynamic> Handle(SendEmailWithAttachmentEvent @event)
        {
            _logger.LogDebug($"Subscriber received a SendEmailEvent notification.");
            if (@event.Id != Guid.Empty)
            {
                _logger.LogDebug($"Event ID: {@event.Id}");
                using IServiceScope scope = GetScope(@event.ChannelKey);
                _logger.LogDebug($"ChannelKey: {@event.ChannelKey}");
                var emailManager = scope.ServiceProvider.GetService<EmailManager>();

                //var emailQuotaInteractor = scope.ServiceProvider.GetService<IEmailQuotaInteractor>();
                //var emailQuota = emailQuotaInteractor.GetEmailQuota(@event.ChannelKey)?.Result;

                var emailProviderService = scope.ServiceProvider.GetService<IEmailProviderInteractor>();
                var providerName = emailProviderService.GetEmailProviderByChannel(@event.ChannelKey)?.Result?.Name;
                _logger.LogDebug($"Provider Name: {providerName}");
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
                    Subject = @event.Subject,
                    Tag = @event.Tag,
                    Files = @event.Files
                };
                _logger.LogInformation($"Trying to send email with attachments.");
                emailManager.SendEmailWithAttachments(email);
                //emailManager.SendEmail(email);
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
            string databaseProvider = configuration.GetSection("DatabaseProvider")?.Value;
            services.AddScoped(_ => configuration);
            services.AddLogging();
            services.AddEmailBackgroundTask(channelKey);
            services.AddEmailServices(Program.AppName, databaseProvider);

            services.AddScoped<EmailDBInfo>(provider =>
            {
                string microservice = Program.AppName;
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                string databaseProvider = configuration.GetSection("DatabaseProvider")?.Value;
                return new EmailDBInfo(connectionString, databaseProvider);
            });

            services.AddScoped<EmailProviderConnectionString>(provider =>
            {
                var emailProviderService = provider.GetService<IEmailProviderInteractor>();
                var emailProviderConfiguration = emailProviderService.GetEmailProviderByChannel(channelKey)?.Result?.Configuration;
                //_logger.LogDebug($"emailProviderConfiguration : {emailProviderConfiguration}");
                return new EmailProviderConnectionString(emailProviderConfiguration);
            });
            var scope = services.BuildServiceProvider().CreateScope();
            return scope;
        }
    }
}
