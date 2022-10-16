using System.ComponentModel.DataAnnotations;
using HBDStack.MediatR.DDD;
using HBDStack.ObjectMapper.Abstraction;
using HBDStack.Results;
using MediatR.AppServices.Features.Profiles.Events;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Repos;
using MediatR.Domains.Services;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Actions;

public class CreateProfileCommandV3 : BaseCommand, IRequestFluent<Profile>
{
    [Required] public string Email { get; set; } = default!;

    [Phone] public string Phone { get; set; } = default!;

    internal string MembershipNo { get; set; } = default!;

    [StringLength(150)] [Required] public string Name { get; set; } = default!;
}

internal sealed class CreateProfileCommandHandlerV3 : IRequestFluentHandler<CreateProfileCommandV3, Profile>
{
    private readonly IMembershipService _membershipProvider;
    private readonly IObjectMapper _mapper;
    private readonly IProfileRepo _repository;

    public CreateProfileCommandHandlerV3(
        IProfileRepo repository,
        IMembershipService membershipProvider,
        IObjectMapper mapper)
    {
        _membershipProvider = membershipProvider;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IResult<Profile>> Handle(CreateProfileCommandV3 request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(request.Email))
            return Result.Fails<Profile>($"Email {request.Email} is already existed.", new[] { nameof(request.Email) });

        var profile = _mapper.Map<Profile>(request);
        //Add
        await _repository.AddAsync(profile, cancellationToken);
        //Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        //Return result
        return Result.Ok(profile);
    }
}