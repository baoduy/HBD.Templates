using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TEMP.Api.Configs.Handlers;

public sealed class HealthCheckHandler : IHealthCheck
{
    private readonly ILogger<HealthCheckHandler> _logger;
    public HealthCheckHandler(ILogger<HealthCheckHandler> logger) => _logger = logger;
    

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        //TODO: Do the health check and return the result here
        var healthCheckResultHealthy = true;

        if (healthCheckResultHealthy)
        {
            var goodms = "TEMP Services is in GOOD health";
            _logger.LogInformation(goodms);

            return Task.FromResult(
                HealthCheckResult.Healthy(goodms));
        }

        var ms = "TEMP Services is in BAD health";
        _logger.LogInformation(ms);

        return Task.FromResult(
            HealthCheckResult.Unhealthy(ms));
    }
}