using AutoMapper;
using TEMP.Domains.Share;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.Domains.Features.Profiles.Events
{
    [AutoMap(typeof(Profile), ReverseMap = true)]
    public sealed record ProfileCreatedEvent(Guid Id, string Name) : DomainEvent;

}