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
public class CreateProfileCommand : BaseCommand, IRequest<ProfileBasicView>
{
    [Required] public string Email { get; set; }

    [Phone] public string Phone { get; set; }

    internal string MembershipNo { get; set; }

    [StringLength(150)] [Required] public string Name { get; set; }
}

internal sealed class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, ProfileBasicView>
{
    private readonly IMembershipService _membershipProvider;
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repository;

    public CreateProfileCommandHandler(
        IProfileRepo repository,
        IMembershipService membershipProvider,
        IMapper mapper)
    {
        _membershipProvider = membershipProvider;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProfileBasicView> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(request.Email))
            throw new BizCommandException($"Email {request.Email} is already existed.", nameof(request.Email));

        var profile = new Profile(request.Name,
            request.MembershipNo,
            request.Email,
            request.Phone,
            request.UserId);

        //Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        //Save
        await _repository.AddAsync(profile, cancellationToken);
        await _repository.SaveAsync(cancellationToken);

        //Return result
        return _mapper.Map<ProfileBasicView>(profile);
    }
}