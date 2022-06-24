using HBD.EfCore.Abstractions.Events;

namespace MediatR.Domains.Abstracts;

public abstract record DomainEvent : IEventItem
{
}