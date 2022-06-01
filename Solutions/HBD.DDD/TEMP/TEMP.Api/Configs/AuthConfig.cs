using TEMP.Api.Configs.Handlers;
using TEMP.Core.Options;

namespace TEMP.Api.Configs;

internal static class AuthConfig
{
    public static IServiceCollection AddAuths(this IServiceCollection services, IConfiguration configuration)
    {
        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);
        if (!features.RequireAuthorization) return services;

        services.AddJwtAuth(configuration)
            .AddClaimsProvider<ClaimsProvider>();
        
        return services;
    }
}