using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TEMP.Core;
using TEMP.Infras;

namespace TEMP.Api
{
    public static class Program
    {
        internal static bool EnabledAzureAppConfiguration { get; private set; }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
//                 .UseAppInsightsLog(c =>
//                 {
// #if DEBUG
//                     c.AddConsole();
// #endif
//                 })
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile("appsettings.json", true)
                        .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", true)
                        .AddEnvironmentVariables();
                    
                    var appConfig =  config.Build().GetConnectionString("AppConfig");
                    if (string.IsNullOrWhiteSpace(appConfig)) return;
                    
                    //Load Feature Management from Azure App Configuration.
                    config.AddAzureAppConfiguration(options =>
                        options
                            .Select(KeyFilter.Any, SettingKeys.ApiName)
                            .Connect(appConfig)
                            .UseFeatureFlags());
                    
                    EnabledAzureAppConfiguration = true;
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        public static async Task Main(string[] args)
        {
            //Why not run Migration after with IHost?
            //  1. Migration is only infra level, and only the DbContext instance is required.
            //  2. Create IHost takes time and loaded many third-party services into the memory that are not involved in the migration process.
            //  3. The migration job is a pre-deployment job, and it should run as fast as possible.

            //======= Start migration Job
            var isMigrateJob = string.Equals(args.FirstOrDefault(), "migration", StringComparison.OrdinalIgnoreCase);
            //If argument is migration then run the migration and stop.
            if (isMigrateJob)
            {
                Console.Write("Started migration job");
                await RunMigration().ConfigureAwait(false);
                Console.Write("Completed migration job");
                return;
            }
            //======= End migration Job

            var host = CreateHostBuilder(args).Build();

#if DEBUG
            var config = host.Services.GetRequiredService<IConfiguration>();
            await InfraMigration.MigrateDb(config.GetConnectionString("TEMPDb")).ConfigureAwait(false);
#endif

            await host.RunAsync().ConfigureAwait(false);
        }

        private static async Task RunMigration()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{env}.json", true)
                .AddEnvironmentVariables()
                .Build();

            await InfraMigration.MigrateDb(config.GetConnectionString("TEMPDb")).ConfigureAwait(false);
        }
    }
}