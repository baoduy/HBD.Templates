using TEMP.Api.Configs;

var builder = WebApplication
    .CreateBuilder(args)
    //Azure App Configuration
    .AddAzAppConfig()
    //Azure App Insight Logs
    .AddLogs();

//Run migration and exit the app if needed.
await builder.RunMigrationAsync(args);

// Add services to the container.
builder.Services
    .AddSwagger()
    .AddAuths(builder.Configuration)
    .AddAspNetConfig(builder.Configuration)
    .AddOptions(builder.Configuration)
    .AddAllAppServices(builder.Configuration)
    //.AddHangfire(builder.Configuration)
    .AddHealthzChecks();

var app = builder.Build()
    .EnableFeatures(builder.Configuration)
    .EnableDevFeatures(); //Dev Features like swagger error pages,...

app.UseAuthentications(builder.Configuration);
app.UseRouting();
app.MapControllerRoute(
    "default",
    "api/{controller}/{action=Get}/{id?}");

app.UseMiddlewares(builder.Configuration)
    .UseEndpointsWithHealthCheck();

await app.RunWithServiceBusAsync();
//await app.RunAsync();

//This Startup endpoint for Unit Tests
namespace TEMP.Api
{
    public partial class Program
    {
    }
}