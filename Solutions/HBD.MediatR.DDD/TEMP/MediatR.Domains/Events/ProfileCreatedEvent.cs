using AutoMapper;
using MediatR.Domains.Abstracts;

namespace MediatR.Domains.Events
{
    [AutoMap(typeof(Aggregators.Profile), ReverseMap = true)]
    public sealed record ProfileCreatedEvent(Guid Id, string Name) : DomainEvent;

}