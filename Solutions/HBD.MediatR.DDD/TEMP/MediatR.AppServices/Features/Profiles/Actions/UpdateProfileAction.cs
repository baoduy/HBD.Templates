using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HBDStack.MediatR.DDD;
using HBDStack.Results;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Repos;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Actions;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class UpdateProfileCommand : BaseCommand,IRequestFluent<ProfileBasicView>
{
    [Required] public Guid Id { get; set; } = default!;

    [Phone] public string? Phone { get; set; }

    public string? Name { get; set; } = default!;
}

internal sealed class UpdateProfileCommandHandler : IRequestFluentHandler<UpdateProfileCommand, ProfileBasicView>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repo;

    public UpdateProfileCommandHandler(
        IMapper mapper,
        IProfileRepo repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<IResult<ProfileBasicView>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == default)
            return Result.Fails<ProfileBasicView>("The Id is in valid.", new[]{nameof(request.Id)});

        var profile = await _repo.FindAsync(request.Id);

        if (profile == null)
            return Result.Fails<ProfileBasicView>($"The Profile {request.Id} is not found.", new[]{nameof(request.Id)});

        //Update Here
        profile.Update(null,request.Name, request.Phone,null, request.UserId!);

        //Add Event

        //Save to Db - EfAutoSave will do this
        //await _repo.SaveAsync(cancellationToken);

        //Return result
        return Result.Ok( _mapper.Map<ProfileBasicView>(profile));
    }
}