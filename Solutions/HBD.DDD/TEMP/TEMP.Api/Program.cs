

using TEMP.Api.Configs;
using TEMP.Core;
using TEMP.Infras;

Console.WriteLine("start program");
var isMigration = args.Any(x => string.Equals(x, "migration", StringComparison.OrdinalIgnoreCase));
//Run the migration job under K8s execution
if (isMigration)
{
    Console.WriteLine("Start App with Db migration...");

    var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true)
        .AddJsonFile($"appsettings.{envName}.json", true)
        .AddEnvironmentVariables()
        .Build();

    //Stop here after migrated.
    await InfraMigration.MigrateDb(config.GetConnectionString(SettingKeys.DbConnectionString));
    return;
}else
   Console.WriteLine("Start App...");

var builder = WebApplication.CreateBuilder(args);
//Log
//builder.Services.AddLogging();

// Add services to the container.
builder.Services
    .AddSwagger()
    //.AddAuth(builder.Configuration)
    //.AddSingaAuth(builder.Configuration)
    .AddAspNetConfig(builder.Configuration)
    .AddOptions(builder.Configuration)
    .AddAllAppServices(builder.Configuration)
    //.AddHangfire(builder.Configuration)
    .AddHealthCheckConfigs();

var app = builder.Build()
    .EnableFeatures(builder.Configuration)
    .EnableDevFeatures(); //Dev Features like swagger error pages,...

// Cronjob
// if (args.Length > 0 && args[0].IsJobTask())
// {
//     await app.RunTaskAsync(args[0]);
//     return;
// }

app.UseRouting();
app.UseAuthentications(builder.Configuration);

app.MapControllerRoute(
    "default",
    "api/{controller}/{action=Index}/{id?}");

app.UseMiddlewares(builder.Configuration)
    .UseEndpointsWithHealthCheck();

await app.RunAsync();

//This Startup endpoint for Unit Tests
namespace Singa.Portal
{
    public partial class Program
    {
    }
}