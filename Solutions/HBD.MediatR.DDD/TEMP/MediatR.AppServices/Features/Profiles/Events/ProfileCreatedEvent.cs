using AutoMapper;
using MediatR.Domains.Share;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Events;

[AutoMap(typeof(Profile), ReverseMap = true)]
public sealed record ProfileCreatedEvent(Guid Id, string Name) : DomainEvent, INotification;