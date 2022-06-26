using AutoMapper;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;
// ReSharper disable ClassNeverInstantiated.Global

namespace MediatR.AppServices.Features.Profiles.Models;

[AutoMap(typeof(Profile))]
public class ProfileBasicView
{
    public string Name { get; set; }
}