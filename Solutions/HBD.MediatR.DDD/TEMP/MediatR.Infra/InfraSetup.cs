using HBDStack.SlimMessageBus.AzureBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR.Domains.Share;
using MediatR.Infra.ServiceBus.Receivers;
using MediatR.Infra.Share;
using SlimMessageBus.Host.AzureServiceBus;
using SlimMessageBus.Host.MsDependencyInjection;
using SlimMessageBus.Host.Serialization.SystemTextJson;

namespace MediatR.Infra;

public static class InfraSetup
{
    private const string Name = "MediatR.Infra";

    public static IServiceCollection AddInfraServices(this IServiceCollection service, string connectionString)
    {
        //Add MediatR - EfCore Auto Save
        service.AddMediatEfCoreExtensions<TEMPContext>();
        
        service
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
                enableAutoMapper: true,
                enableAutoScanEventHandler: true,
                assembliesToScans: new[] {typeof(InfraSetup).Assembly, typeof(DomainSchemas).Assembly});
        
        return service;
    }

    public static IServiceCollection AddInfraServiceBus(this IServiceCollection service, IConfiguration configuration)
        => service.AddEventPublisher<EventPublisher>()//Domain Event handler
            .AddSlimMessageBus((mbb, svp) =>
            {
                mbb.Produce<Sub1Message>(x => x.DefaultTopic("topic-1").WithBusMessageModifier())
                    .Consume<Sub1Message>(c => c.Topic("topic-1")
                        .SubscriptionName("sub-1")
                        .PrefetchCount(10)
                        .Instances(1)
                        .WithConsumer<Sub1Receiver>()
                    )
                    .Consume<Sub1Message>(c => c.Topic("topic-1")
                        .SubscriptionName("sub-2")
                        //.SubscriptionSqlFilter($"{nameof(Sub1Message.FilterProperty)}='Steven'")
                        .PrefetchCount(10)
                        .Instances(1)
                        .WithConsumer<Sub1Receiver>()
                    )
                    .WithProviderServiceBus(
                        new ServiceBusMessageBusSettings(configuration.GetConnectionString("AzureBus"))
                        {
                            TopologyProvisioning = new ServiceBusTopologySettings
                            {
                                Enabled = true,
                                // CanConsumerCreateQueue = false,
                                // CanConsumerCreateTopic = false,
                                // CanProducerCreateTopic = false,
                                // CanProducerCreateQueue = false,
                                CanConsumerCreateSubscription = true,
                            }
                        })
                    // Add other bus transports, if needed
                    //.AddChildBus("Bus2", (builder) => {})
                    .WithSerializer(new JsonMessageSerializer())
                    ;
            }, addConsumersFromAssembly: new[] { typeof(InfraSetup).Assembly });

   

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