﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HBDStack.MediatR.DDD;
using HBDStack.Results;
using MediatR.AppServices.Features.Profiles.Events;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Repos;
using MediatR.Domains.Services;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Actions;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class CreateProfileCommand : BaseCommand, IRequestFluent<ProfileBasicView>
{
    [Required] public string Email { get; set; } = default!;

    [Phone] public string Phone { get; set; } = default!;

    internal string MembershipNo { get; set; } = default!;

    [StringLength(150)] [Required] public string Name { get; set; } = default!;
}

internal sealed class CreateProfileCommandHandler : IRequestFluentHandler<CreateProfileCommand, ProfileBasicView>
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

    public async Task<IResult<ProfileBasicView>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        //Check duplicate
        if (await _repository.IsEmailExistAsync(request.Email))
            return Result.Fails<ProfileBasicView>($"Email {request.Email} is already existed.", new[] { nameof(request.Email) });

        var profile = _mapper.Map<Profile>(request);
        //Add
        await _repository.AddAsync(profile, cancellationToken);

        //Event - Issue the Id maybe empty if the Id is generated by Database
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        //Save
        await _repository.SaveAsync(cancellationToken);
        //Return result
        
        //NOTE this will return a lazy mapping result and only map profile to ProfileBasicView after SaveChanges is called.
        return _mapper.ResultOf<ProfileBasicView>(profile);
    }
}