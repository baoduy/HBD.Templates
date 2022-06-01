using AutoMapper;
using TEMP.AppServices.Abstracts;
using Profile = TEMP.Domains.Aggregators.Profile;
// ReSharper disable ClassNeverInstantiated.Global

namespace TEMP.AppServices.Models.Profiles;

[AutoMap(typeof(Profile))]
public class ProfileBasicView : ViewModelBase
{
    public string Name { get; set; }
}