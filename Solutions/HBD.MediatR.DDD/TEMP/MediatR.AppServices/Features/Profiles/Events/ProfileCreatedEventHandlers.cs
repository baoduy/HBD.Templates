using SlimMessageBus;

namespace MediatR.AppServices.Features.Profiles.Events;

internal sealed class ProfileCreatedEventAuditTrailHandler : INotificationHandler<ProfileCreatedEvent>
{
    //TODO remove this as just for testing purposed only
    public static bool Called { get; set; }

    public Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        Called = notification.Id != default;
        //TODO; Add audit trail logic here
        return Task.CompletedTask;
    }
}

internal sealed class ProfileCreatedEventServiceBusHandler : INotificationHandler<ProfileCreatedEvent>
{
    private readonly IMessageBus _bus;


    //TODO remove this as just for testing purposed only
    public static bool Called { get; set; }

    public ProfileCreatedEventServiceBusHandler(IMessageBus bus)
    {
        _bus = bus;
    }


    public async Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        //await _busSender.SendAsync(notification, cancellationToken);
        Called = true;
    }
}