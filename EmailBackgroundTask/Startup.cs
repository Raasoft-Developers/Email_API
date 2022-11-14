using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmailBackgroundTask.AutofacModules;
using EmailBackgroundTask.EventHandler;
using EmailBackgroundTask.Events;
using EmailBackgroundTask.Extensions;
using EmailBackgroundTask.Models;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace EmailBackgroundTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services/*.AddCustomHealthCheck(Configuration)*/
                .Configure<BackgroundTaskSettings>(Configuration)
                .AddOptions()
                .AddEventBus(Configuration);

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule());
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseSerilogRequestLogging();
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });*/
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SendEmailEvent, SendEmailEventHandler>();
            eventBus.Subscribe<SendEmailWithAttachmentEvent, SendEmailWithAttachmentEventHandler>();
        }
    }
}
