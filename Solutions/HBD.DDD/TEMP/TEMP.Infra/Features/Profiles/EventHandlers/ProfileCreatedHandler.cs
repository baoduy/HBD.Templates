using HBD.EfCore.Abstractions.Events;
using HBD.EfCore.Events.Handlers;
using StatusGeneric;
using TEMP.Domains.Features.Profiles.Events;

namespace TEMP.Infra.Features.Profiles.EventHandlers;

internal class ProfileCreatedEventAuditTrailHandler : IBeforeSaveEventHandlerAsync<ProfileCreatedEvent>
{
    //This is just for demo testing purposes. Please remove it from real application
    public static bool Called { get; set; }

    public ValueTask<IStatusGeneric> HandleAsync(IEventEntity callingEntity, ProfileCreatedEvent domainEvent,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var status = new StatusGenericHandler();

        //TODO: implement your logic here
        Called = true;
        return new ValueTask<IStatusGeneric>(status);
    }
}