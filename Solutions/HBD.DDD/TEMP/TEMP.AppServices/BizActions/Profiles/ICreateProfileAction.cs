using HBD.EfCore.BizActions.Abstraction;
using TEMP.AppServices.Models.Profiles;
using TEMP.Domains.Aggregators;

namespace TEMP.AppServices.BizActions.Profiles;

/// <summary>
/// This is auto action => no need to call _repo.SaveAsync()
/// </summary>
public interface ICreateProfileAction : IGenericActionWriteDbAsync<ProfileModel, Profile>
{
}