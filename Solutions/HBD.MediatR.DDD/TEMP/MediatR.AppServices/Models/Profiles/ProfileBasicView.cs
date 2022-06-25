using AutoMapper;
using Profile = MediatR.Domains.Aggregators.Profile;
// ReSharper disable ClassNeverInstantiated.Global

namespace MediatR.AppServices.Models.Profiles;

[AutoMap(typeof(Profile))]
public class ProfileBasicView
{
    public string Name { get; set; }
}