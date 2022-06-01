using TEMP.Api.Configs;

var builder = WebApplication.CreateBuilder(args)
    .AddAzAppConfig();

//Log
//builder.Services.AddLogging();

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

// Cronjob
// if (args.Length > 0 && args[0].IsJobTask())
// {
//     await app.RunTaskAsync(args[0]);
//     return;
// }

app.UseAuthentications(builder.Configuration);
app.UseRouting();
app.MapControllerRoute(
    "default",
    "api/{controller}/{action=Get}/{id?}");

app.UseMiddlewares(builder.Configuration)
    .UseEndpointsWithHealthCheck();

await app.RunAsync();

//This Startup endpoint for Unit Tests
namespace TEMP.Api
{
    public partial class Program
    {
    }
}