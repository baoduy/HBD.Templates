using System;
using AutoMapper;
using TEMP.Domains.Abstracts;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.Domains.Events
{
    [AutoMap(typeof(Profile), ReverseMap = true)]
    public sealed record ProfileCreatedEvent(Guid Id, string Name) : DomainEvent;

}