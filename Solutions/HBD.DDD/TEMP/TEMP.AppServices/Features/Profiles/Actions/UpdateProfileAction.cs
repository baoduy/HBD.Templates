using AutoMapper;
using HBD.EfCore.BizActions.Abstraction;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Share;
using TEMP.Domains.Features.Profiles.Repos;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.AppServices.Features.Profiles.Actions;

/// <summary>
/// This is auto action => no need to call _repo.SaveAsync()
/// </summary>
public interface IUpdateProfileAction : IGenericActionWriteDbAsync<UpdateProfileModel, Profile?>
{
}

internal sealed class UpdateProfileAction : BizActionAsync, IUpdateProfileAction
{
    private readonly IProfileRepo _repo;

    public UpdateProfileAction(IPrincipalProvider principalProvider,
        IMapper mapper,
        IProfileRepo repo)
        : base(principalProvider, mapper) =>
        _repo = repo;

    public async Task<Profile?> BizActionAsync(UpdateProfileModel inputData, CancellationToken cancellationToken)
    {
        if (inputData.Id == default)
        {
            AddError("The Id is in valid.",nameof(inputData.Id));
            return null;
        }

        var profile =await _repo.FindAsync(p=>p.Id == inputData.Id, cancellationToken);

        if (profile == null)
        {
            AddError($"The Profile {inputData.Id} is not found.",nameof(inputData.Id));
            return null;
        }
            
        //Update Here
        profile.Update(null,inputData.Name, inputData.Phone,null, inputData.UserId!);

        return profile;
    }
}