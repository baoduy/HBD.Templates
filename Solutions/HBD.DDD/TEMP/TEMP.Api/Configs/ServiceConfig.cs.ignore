using HBD.EfCore.DataAuthorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TEMP.Api.Providers;
using TEMP.AppServices;
using TEMP.Core;
using TEMP.Infras;

namespace TEMP.Api.Configs
{
    internal static class ServiceConfig
    {
        #region Methods

        public static IServiceCollection AddAllAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPrincipalProvider, PrincipalProvider>()
                .AddScoped<IDataKeyProvider>(p => p.GetService<IPrincipalProvider>());

            var conn = configuration.GetConnectionString(SettingKeys.DbConnectionString);

            return services
                .AddAppServices()
                .AddInfraServices(conn);
        }

        #endregion Methods
    }
}