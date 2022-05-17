using HBD.EfCore.BizActions.Abstraction;
using TEMP.AppServices.Models.Profiles;
using TEMP.Domains.Aggregators;

namespace TEMP.AppServices.BizActions.Profiles
{
    /// <summary>
    /// This is not auto action => need to call _repo.SaveAsync()
    /// </summary>
    public interface IDeleteProfileAction : IGenericActionAsync<ProfileDeleteModel, Profile>
    {
    }
}