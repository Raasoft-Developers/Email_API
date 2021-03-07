using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nvg.Api.Email.Extensions;
using Nvg.EmailService.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Nvg.Api.Email
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.') + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateLogger();

            try
            {
                var host = CreateHostBuilder(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);

                Log.Information("Seeding database...");

                string path = Directory.GetCurrentDirectory();

                host.MigrateDbContext<EmailDbContext>((context, services) =>
                {
                    var emailTemplateDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "EmailTemplate.json"));
                    dynamic templateData = JsonConvert.DeserializeObject<object>(emailTemplateDataFromJson);
                    var emailChannelDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "EmailChannel.json"));
                    dynamic channelData = JsonConvert.DeserializeObject<object>(emailChannelDataFromJson);
                    var emailPoolDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "EmailPool.json"));
                    dynamic poolData = JsonConvert.DeserializeObject<object>(emailPoolDataFromJson);
                    var emailProviderDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "EmailProvider.json"));
                    dynamic providerData = JsonConvert.DeserializeObject<object>(emailProviderDataFromJson);
                    new EmailDbContextSeed()
                        .SeedAsync(context, configuration, channelData, poolData, providerData, templateData)
                        .Wait();
                });
                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /*
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac();
        */

        private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddAutofac())
            .UseConfiguration(configuration)
            .CaptureStartupErrors(false)
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();

        private static Serilog.ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                .WriteTo.File(
                    @"C:\NovigoIdentityPortal\IdentityServerPortalLogs\EmailLog_"+DateTime.Now.ToString("yyyyMMddHHmmss") +".txt",
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            return builder.Build();
        }
    }
}
