﻿using Autofac;
using EventBus.Abstractions;
using EventBus.Subscription;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nvg.EmailService;
using Nvg.EmailService.Data;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nvg.Api.Email.Helpers
{
    public static class StartupHelper
    {
        public static void AddEmailService(this IServiceCollection services, string microservice, IConfiguration configuration)
        {
            services.AddScoped<EmailDBInfo>(provider =>
            {
                var logger = provider.GetService<ILogger<EmailDBInfo>>();
                logger.LogDebug($"RESOLVING EmailDBInfo");
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                return new EmailDBInfo(connectionString);
            });
            EmailServiceExtension.AddEmailServices(services, microservice);
        }

        public static void RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            string subscriptionClientName = configuration.GetValue<string>("EventBusQueue", string.Empty);
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri(configuration["EventBusConnection"]);
                factory.DispatchConsumersAsync = true;
                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }
                return new DefaultRabbitMQPersistentConnection(factory, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<ISubscriptionManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }
                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
        }
    }
}