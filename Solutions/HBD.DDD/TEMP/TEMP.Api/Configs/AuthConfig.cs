using HBDStack.Web.Auths;
using TEMP.Api.Configs.Handlers;
using TEMP.Core.Options;

namespace TEMP.Api.Configs;

internal static class AuthConfig
{
    public static IServiceCollection AddAuths(this IServiceCollection services, IConfiguration configuration)
    {
        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);
        if (!features.RequireAuthorization) return services;

        services
            .AddClaimsProvider<ClaimsProvider>()
            .AddAuth(new AuthsOptions()).AddJwtAuths(configuration)
            ;
        
        return services;
    }
}