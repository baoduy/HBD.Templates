using HBD.AzProxy.ServiceBus;

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
    private readonly IBusMessageSender _busSender;

    //TODO remove this as just for testing purposed only
    public static bool Called { get; set; }

    public ProfileCreatedEventServiceBusHandler(IBusMessageSenderFactory factory)
        => _busSender = factory.CreateSender("tp1");

    public async Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        //await _busSender.SendAsync(notification, cancellationToken);
        Called = true;
    }
}