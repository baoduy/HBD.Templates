using AutoMapper;
using MediatR.AppServices.Abstracts;
using Profile = MediatR.Domains.Aggregators.Profile;
// ReSharper disable ClassNeverInstantiated.Global

namespace MediatR.AppServices.Models.Profiles;

[AutoMap(typeof(Profile))]
public class ProfileBasicView : ViewModelBase
{
    public string Name { get; set; }
}