using HBD.EfCore.Abstractions.Events;

namespace MediatR.Domains.Share;

public abstract record DomainEvent : IEventItem
{
}