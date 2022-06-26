using AutoMapper;
using TEMP.AppServices.Share;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

// ReSharper disable ClassNeverInstantiated.Global

namespace TEMP.AppServices.Features.Profiles.Models;

[AutoMap(typeof(Profile))]
public class ProfileBasicView : ViewModelBase
{
    public Guid Id { get; set; } = default!;
    
    public string? Name { get; set; }
}