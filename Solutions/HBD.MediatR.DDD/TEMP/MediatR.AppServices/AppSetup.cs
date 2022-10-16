using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("MediatR.AppServices.Tests")]

namespace MediatR.AppServices;

public static class AppSetup
{
    private const string Name = "MediatR.AppServices";
        
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        //Mapper
        //TypeAdapterConfig.GlobalSettings
        
        
        //Add MediatR
        services.AddMediatR(typeof(AppSetup).Assembly);

        //Add StateManagement
        services.AddDistributedStateStorage();
        
        // return services.Scan(s => s.FromAssemblies(typeof(AppSetup).Assembly)
        //     .AddClasses(c => c.InNamespaces($"{Name}.BizActions", $"{Name}.ProcessManagers", $"{Name}.QueryServices"))
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime()
        // );
        return services;
    }
}