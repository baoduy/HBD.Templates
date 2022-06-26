using AutoMapper;
using HBD.EfCore.BizActions.Abstraction;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Share;
using TEMP.Domains.Features.Profiles.Events;
using TEMP.Domains.Features.Profiles.Repos;
using TEMP.Domains.Services;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.AppServices.Features.Profiles.Actions;

/// <summary>
/// This is auto action => no need to call _repo.SaveAsync()
/// </summary>
public interface ICreateProfileAction : IGenericActionWriteDbAsync<CreateProfileModel, Profile?>
{
}

internal sealed class CreateProfileAction : BizActionAsync<Profile>, ICreateProfileAction
{
    private readonly IMembershipService _membershipProvider;
    private readonly IProfileRepo _repository;

    public CreateProfileAction(IMembershipService membershipProvider,
        IPrincipalProvider principalProvider,
        IMapper mapper,
        IProfileRepo repository)
        : base(principalProvider, mapper, repository)
    {
        _membershipProvider = membershipProvider;
        _repository = repository;
    }

    public async Task<Profile?> BizActionAsync(CreateProfileModel inputData,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(inputData.MembershipNo))
            inputData.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(inputData.Email))
        {
            AddError($"Email {inputData.Email} is already existed.", nameof(inputData.Email));
            return null;
        }

        //Create
        var profile = Mapper.Map<Profile>(inputData);

        //Add Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        //Add to Repo
        await Repository.AddAsync(profile, cancellationToken);

        //Return original object the BizAction will map to the final object automatically.
        return profile;
    }
}