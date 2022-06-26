namespace MediatR.AppServices.Features.Profiles.Events;

internal sealed class ProfileCreatedEventHandler:INotificationHandler<ProfileCreatedEvent>
{
    //TODO remove this as just for testing purposed only
    public static bool Called { get; set; }

    public Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        Called = true;
       return Task.CompletedTask;
    }
}