using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HBD.EfCore.Repos;
using TEMP.AppServices.Models.Profiles;
using Profile = TEMP.Domains.Aggregators.Profile;


namespace TEMP.AppServices.BizActions.Profiles;

internal class DeleteProfileAction : BizActionAsync<Profile>, IDeleteProfileAction
{
    public DeleteProfileAction(IPrincipalProvider principalProvider, IMapper mapper, IRepository<Profile> repository) : base(principalProvider, mapper, repository)
    {
    }

    public async Task<Profile> BizActionAsync(ProfileDeleteModel inputData, CancellationToken cancellationToken = new CancellationToken())
    {
        if (inputData.Id == default)
        {
            AddError("The Id is in valid.", nameof(inputData.Id));
            return null;
        }

        var profile = await Repository.FindAsync(inputData.Id);

        if (profile == null)
        {
            AddError($"The Profile {inputData.Id} is not found.", nameof(inputData.Id));
            return null;
        }

        Repository.Delete(profile);
        //This is non auto save Action so need to call SaveAsync manually
        await Repository.SaveAsync(cancellationToken);

        return profile;
    }
}