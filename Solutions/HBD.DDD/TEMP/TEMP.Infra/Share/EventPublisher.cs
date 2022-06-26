using HBD.AzProxy.ServiceBus;
using HBD.EfCore.Abstractions.Events;
using HBD.EfCore.Events.Handlers;
using TEMP.Domains.Features.Profiles.Events;

namespace TEMP.Infra.Share;

/// <summary>
/// This will capture all event items after saved.
/// Using this to push event to Service Bus or external api.
/// </summary>
public sealed class EventPublisher : IEventPublisher
{
    private readonly IBusMessageSenderFactory _factory;
    public static bool Called { get; set; }
    public EventPublisher(IBusMessageSenderFactory factory) => _factory = factory;

    public async Task PublishAsync(IEventItem domainEvent)
    {
        switch (domainEvent)
        {
            case ProfileCreatedEvent:
                var sender = _factory.CreateSender("tp1");
                await sender.SendAsync(domainEvent);
                Called = true;
                break;
        }
    }
}