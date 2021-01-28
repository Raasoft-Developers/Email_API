using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nvg.EmailBackgroundTask.Tasks
{
    public class EmailService : BackgroundService
    {
        public ILogger<EmailService> _logger { get; }

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 GracePeriodManagerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("GracePeriodManagerService background task is doing background work.");

                SendEmail();

                //await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

            _logger.LogDebug("GracePeriodManagerService background task is stopping.");

            await Task.CompletedTask;
        }

        private void SendEmail()
        {
            throw new NotImplementedException();
        }
    }
}
