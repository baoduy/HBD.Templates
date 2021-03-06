using System.Runtime.CompilerServices;
using HBD.EfCore.BizAction.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TEMP.AppServices.Share;
using TEMP.Domains;
using TEMP.Infra.Share;

[assembly: InternalsVisibleTo("TEMP.AppServices.Tests")]
[assembly: InternalsVisibleTo("TEMP.Infra.Tests")]

namespace TEMP.Infra;

public static class InfraSetup
{
    private const string Name = "TEMP.Infra";

    public static IServiceCollection AddInfraServices(this IServiceCollection service, string connectionString)
    {
        service
            .AddEventPublisher<EventPublisher>()//Domain Event handler
            .AddGenericRepositories<TEMPContext>()
            .AddImplementations()
            .AddCoreInfraServices<TEMPContext>(op =>
                {
                    op.UseSqlWithMigration(connectionString);

#if DEBUG
                    op.EnableDetailedErrors().EnableSensitiveDataLogging();
#else
                        //TODO: Workaround solution to ignored the Too many IServiceProvider created exception;
                        op.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
#endif
                },
                enableAutoMapper: false,
                enableAutoScanEventHandler: true,
                assembliesToScans: new[] {typeof(InfraSetup).Assembly, typeof(DomainSchemas).Assembly});
        
        return service.AddBizRunner();
    }

    public static IServiceCollection AddInfraServiceBus(this IServiceCollection service, IConfiguration configuration)
        => service.AddServiceBus(configuration, typeof(InfraSetup).Assembly);

    internal static IServiceCollection AddBizRunner(this IServiceCollection services)
    {
        services.RegisterBizRunner<TEMPContext>(new GenericBizRunnerConfig
        {
            DoNotValidateSaveChanges = true,
            SaveChangesExceptionHandler = SaveChangesExceptionHandler.Handler
        });

        return services;
    }

    internal static IServiceCollection AddImplementations(this IServiceCollection services)
    {
        //DONOT: scan the Even handler here as it already scan and added via AddCoreInfraServices
            
        services.Scan(s => s.FromAssemblies(typeof(InfraSetup).Assembly)
            .AddClasses(c => c.Where(t=>t.Namespace!.Contains("Repos")||t.Namespace!.Contains("Services")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services.Scan(s => s.FromAssemblies(typeof(InfraSetup).Assembly)
            .AddClasses(c => c.InNamespaces($"{Name}.ServiceBus.Senders"))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
}