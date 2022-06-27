using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR.AppServices.Features.Profiles.Events;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.AppServices.Share.Exceptions;
using MediatR.Domains.Features.Profiles.Repos;
using MediatR.Domains.Services;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Actions;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class CreateProfileCommandV3 : BaseCommand, IRequest<Profile>
{
    [Required] public string Email { get; set; } = default!;

    [Phone] public string Phone { get; set; } = default!;

    internal string MembershipNo { get; set; } = default!;

    [StringLength(150)] [Required] public string Name { get; set; } = default!;
}

internal sealed class CreateProfileCommandHandlerV3 : IRequestHandler<CreateProfileCommandV3, Profile>
{
    private readonly IMembershipService _membershipProvider;
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repository;

    public CreateProfileCommandHandlerV3(
        IProfileRepo repository,
        IMembershipService membershipProvider,
        IMapper mapper)
    {
        _membershipProvider = membershipProvider;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Profile> Handle(CreateProfileCommandV3 request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(request.Email))
            throw new BizCommandException($"Email {request.Email} is already existed.", nameof(request.Email));

        var profile = _mapper.Map<Profile>(request);

        //Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        //Save
        await _repository.AddAsync(profile, cancellationToken);
        //EfAutoSave will do this
        //await _repository.SaveAsync(cancellationToken);

        //Return result
        //return _mapper.Map<ProfileBasicView>(profile);
        //return LazyFor<ProfileBasicView>(profile);
        return profile;
    }
}