﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using TEMP.Domains;

[assembly: InternalsVisibleTo("TEMP.AppServices.Tests")]

namespace TEMP.AppServices;

public static class AppSetup
{
    private const string Name = "TEMP.AppServices";
        
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.ShouldUseConstructor = c => c.IsPublic;
            cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.CanRead;
        }, typeof(AppSetup).Assembly,typeof(DomainSchemas).Assembly);

        //Add StateManagement
        services.AddDistributedStateStorage();
        
        return services.Scan(s => s.FromAssemblies(typeof(AppSetup).Assembly)
            .AddClasses(c => c.Where(t=>t.Namespace!.Contains("Actions")||t.Namespace!.Contains("Queries")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }
}