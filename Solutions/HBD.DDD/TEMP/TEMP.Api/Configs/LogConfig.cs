using Microsoft.Extensions.Logging.ApplicationInsights;

namespace TEMP.Api.Configs;

internal static class LogConfig
{
    public static WebApplicationBuilder AddLogs(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var instrumentKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
        if (string.IsNullOrWhiteSpace(instrumentKey))
        {
            builder.Host.ConfigureLogging((_, b) => b.AddConsole());
        }
        else
        {

            builder.Services.AddApplicationInsightsTelemetry();
            builder.Host.ConfigureLogging((_, b) =>
            {
#if DEBUG
                b.AddConsole();       
#endif
                b.AddApplicationInsights(instrumentKey);
            });
        }

        return builder;
    }
}