﻿using System.ComponentModel.DataAnnotations;
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
public class CreateProfileCommandV2 : BaseCommand, IRequest<ILazy<ProfileBasicView>>
{
    [Required] public string Email { get; set; } = default!;

    [Phone] public string Phone { get; set; } = default!;

    internal string MembershipNo { get; set; } = default!;

    [StringLength(150)] [Required] public string Name { get; set; } = default!;
}

internal sealed class CreateProfileCommandHandlerV2 :BaseRequestHandler, IRequestHandler<CreateProfileCommandV2, ILazy<ProfileBasicView>>
{
    private readonly IMembershipService _membershipProvider;
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repository;

    public CreateProfileCommandHandlerV2(
        IProfileRepo repository,
        IMembershipService membershipProvider,
        IMapper mapper):base(mapper)
    {
        _membershipProvider = membershipProvider;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ILazy<ProfileBasicView>> Handle(CreateProfileCommandV2 request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(request.Email))
            throw new BizCommandException($"Email {request.Email} is already existed.", nameof(request.Email));

        var profile = _mapper.Map<Profile>(request);
        //Add
        await _repository.AddAsync(profile, cancellationToken);
        //Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));
        //Return result
        return LazyFor<ProfileBasicView>(profile);
    }
}