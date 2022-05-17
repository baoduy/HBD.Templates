using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace TEMP.Api.Handlers
{
    public sealed class HealthCheck : IHealthCheck
    {
        #region Fields

        private readonly ILogger<HealthCheck> _logger;

        #endregion Fields

        #region Constructors

        public HealthCheck(ILogger<HealthCheck> logger)
        {
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}