using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace TEMP.Api.Configs;

internal static class HealthCheckConfig
{
    public static IServiceCollection AddHealthCheckConfigs(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<DbContext>();

        return services;
    }

    /// <summary>
    /// The health check endpoint will be "/healthz"
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHealthzCheck(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/healthz", new HealthCheckOptions { AllowCachingResponses = false });
        return endpoints;
    }
}