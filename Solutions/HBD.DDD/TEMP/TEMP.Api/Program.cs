using TEMP.Api.Configs;
using TEMP.Core;
using TEMP.Infras;

var builder = WebApplication.CreateBuilder(args);
//Log
//builder.Services.AddLogging();

#if DEBUG
var isMigration = true;
#else
var isMigration = args.Any(x => string.Equals(x, "migration", StringComparison.OrdinalIgnoreCase));
#endif
//Run the migration job under K8s execution
if (isMigration)
{
    Console.WriteLine("Start App with Db migration...");

    //Stop here after migrated.
    await InfraMigration.MigrateDb(builder.Configuration.GetConnectionString(SettingKeys.DbConnectionString));
    //Stop the app after ran migration
#if !DEBUG
    return;
#endif
}
else
    Console.WriteLine("Start App...");

// Add services to the container.
builder.Services
    .AddSwagger()
    //.AddAuth(builder.Configuration)
    //.AddSingaAuth(builder.Configuration)
    .AddAspNetConfig(builder.Configuration)
    .AddOptions(builder.Configuration)
    .AddAllAppServices(builder.Configuration)
    //.AddHangfire(builder.Configuration)
    .AddHealthzChecks();

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