using AutoMapper;
using HBDStack.EfCore.BizActions.Abstraction;
using HBDStack.EfCore.Repos.Abstractions;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Share;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.AppServices.Features.Profiles.Actions;

/// <summary>
/// This is not auto action => need to call _repo.SaveAsync()
/// </summary>
public interface IDeleteProfileAction : IGenericActionInOnlyAsync<ProfileDeleteModel>
{
}

internal class DeleteProfileAction : BizActionAsync<Profile>, IDeleteProfileAction
{
    public DeleteProfileAction(IPrincipalProvider principalProvider, IMapper mapper, IRepository<Profile> repository) : base(principalProvider, mapper, repository)
    {
    }

    public async Task BizActionAsync(ProfileDeleteModel inputData, CancellationToken cancellationToken = default)
    {
        if (inputData.Id == default)
        {
            AddError("The Id is in valid.", nameof(inputData.Id));
            return;
        }

        var profile = await Repository.FindAsync(inputData.Id);

        if (profile == null)
        {
            AddError($"The Profile {inputData.Id} is not found.", nameof(inputData.Id));
            return;
        }

        Repository.Delete(profile);
        //This is non auto save Action so need to call SaveAsync manually
        await Repository.SaveAsync(cancellationToken);
    }
}