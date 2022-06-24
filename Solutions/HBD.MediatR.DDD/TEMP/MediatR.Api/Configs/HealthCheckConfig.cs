using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Api.Configs;

internal static class HealthCheckConfig
{
    public static IServiceCollection AddHealthzChecks(this IServiceCollection services)
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
        var options = new HealthCheckOptions
        {
            AllowCachingResponses = false,
            Predicate = _ => true,
            //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        };
        endpoints.MapHealthChecks("/healthz", options);
        endpoints.MapHealthChecks("/", options);
        return endpoints;
    }
}