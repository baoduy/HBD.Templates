using MediatR.Core;
using MediatR.Core.Options;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace MediatR.Api.Configs;

internal static class AzureAppConfig
{
    public static WebApplicationBuilder AddAzAppConfig(this WebApplicationBuilder builder)
    {
        var feature = builder.Configuration.Bind<FeatureOptions>(FeatureOptions.Name);
        if (!feature.EnableAzureAppConfig) return builder;
        
        var connectionString = builder.Configuration.GetConnectionString("AppConfig");
        
        if (string.IsNullOrWhiteSpace(connectionString)) return builder;

        builder.Host.ConfigureAppConfiguration(b =>
        {
            b.AddAzureAppConfiguration(options =>
            {
                options.Select(KeyFilter.Any, SettingKeys.ApiName);
                options.Connect(connectionString)
                    //.ConfigureRefresh(o => { o.SetCacheExpiration(TimeSpan.FromMinutes(30));})
                    .UseFeatureFlags();
            });
        });

        return builder;
    }
}