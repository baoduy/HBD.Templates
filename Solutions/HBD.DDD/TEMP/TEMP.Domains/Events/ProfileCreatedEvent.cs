using System;
using AutoMapper;
using TEMP.Domains.Abstracts;
using TEMP.Domains.ValueObjects;
using Profile = TEMP.Domains.Aggregators.Profile;

//Add this to allows to define Record with inline read only properties
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}

namespace TEMP.Domains.Events
{
    [AutoMap(typeof(Profile), ReverseMap = true)]
    public sealed record ProfileCreatedEvent(Guid Id, PersonName Name) : DomainEvent;

}