using MediatR.Domains.Share;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Events;

public sealed record ProfileCreatedEvent(Guid Id, string Name) : DomainEvent, INotification;