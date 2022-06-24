using HBD.EfCore.Abstractions.Events;
using HBD.EfCore.Events.Handlers;
using StatusGeneric;
using MediatR.Domains.Events;

namespace MediatR.Infra.EventHandlers;

/// <summary>
/// Using this to capture the ProfileCreatedEvent events.
/// This is for internal communication between Domains boundary.
/// </summary>
public class ProfileCreatedHandler : IBeforeSaveEventHandlerAsync<ProfileCreatedEvent>
{
    //This is just for demo testing purposes. Please remove it from real application
    public static int Called { get; set; }

    public ProfileCreatedHandler() => Called = 0;

    public ValueTask<IStatusGeneric> HandleAsync(IEventEntity callingEntity, ProfileCreatedEvent domainEvent,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var status = new StatusGenericHandler();

        //TODO: implement your logic here
        Called += 1;
        return new ValueTask<IStatusGeneric>(status);
    }

    public IStatusGeneric Handle(IEventEntity callingEntity, ProfileCreatedEvent domainEvent)
    {
        var status = new StatusGenericHandler();

        //TODO: implement your logic here
        Called += 1;
        return status;
    }
}