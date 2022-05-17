using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TEMP.Api.Configs
{
    /// <summary>
    /// </summary>
    internal static class CrosConfig
    {
        #region Properties

        private static string Name { get; } = "Cros";

        #endregion Properties

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCrosConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration["AllowedHosts"];

            return services.AddCors(op => op.AddPolicy(Name, p =>
            {
                if (origins == "*")
                    p.AllowAnyOrigin();
                else p.WithOrigins(origins.Split(',', ';'));

                p.AllowAnyHeader();
                p.AllowAnyMethod();
            }));
        }

        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCrosConfig(this IApplicationBuilder app)
        {
            return app.UseCors(Name);
        }

        #endregion Methods
    }
}