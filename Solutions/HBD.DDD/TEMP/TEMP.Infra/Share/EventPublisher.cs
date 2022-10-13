using HBDStack.EfCore.Abstractions.Events;
using HBDStack.EfCore.Events.Handlers;
using SlimMessageBus;
using TEMP.Domains.Features.Profiles.Events;

namespace TEMP.Infra.Share;

/// <summary>
/// This will capture all event items after saved.
/// Using this to push event to Service Bus or external api.
/// </summary>
public sealed class EventPublisher : IEventPublisher
{
    private readonly IMessageBus _bus;
    public static bool Called { get; set; }

    public EventPublisher(IMessageBus bus) => _bus = bus;

    public async Task PublishAsync(IEventItem domainEvent)
    {
        switch (domainEvent)
        {
            case ProfileCreatedEvent:
                await _bus.Publish(domainEvent);
                Called = true;
                break;
        }
    }
}