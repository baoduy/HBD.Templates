using HBDStack.EfCore.Abstractions.Events;
using HBDStack.EfCore.Events.Handlers;

namespace MediatR.Infra.ServiceBus;

/// <summary>
/// This will capture all event items after saved.
/// Using this to push event to Service Bus or external api.
/// </summary>
public sealed class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;

    public EventPublisher(IMediator mediator) => _mediator = mediator;

    public Task PublishAsync(IEventItem domainEvent) => _mediator.Publish(domainEvent);
}