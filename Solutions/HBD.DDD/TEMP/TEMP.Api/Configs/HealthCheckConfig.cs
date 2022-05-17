using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TEMP.Api.Handlers;

namespace TEMP.Api.Configs
{
    internal static class HealthCheckConfig
    {
        #region Methods

        public static IServiceCollection AddHealthCheckConfigs(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DbContext>()
                .AddCheck<HealthCheck>(nameof(HealthCheck));

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                var check = endpoints.MapHealthChecks("/", new HealthCheckOptions {AllowCachingResponses = false});

                ////if (env.IsProduction())
                ////{
                ////    check.RequireAuthorization();
                ////}
            });

            return app;
        }

        #endregion Methods
    }
}