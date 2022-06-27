using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.AppServices.Share.Exceptions;
using MediatR.Domains.Features.Profiles.Repos;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Actions;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class UpdateProfileCommand : BaseCommand,IRequest<ProfileBasicView>
{
    [Required] public Guid Id { get; set; } = default!;

    [Phone] public string? Phone { get; set; }

    public string? Name { get; set; } = default!;
}

internal sealed class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileBasicView>
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

    public async Task<ProfileBasicView> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == default)
            throw new BizCommandException("The Id is in valid.", nameof(request.Id));

        var profile = await _repo.FindAsync(p => p.Id == request.Id, cancellationToken);

        if (profile == null)
            throw new BizCommandException($"The Profile {request.Id} is not found.", nameof(request.Id));

        //Update Here
        profile.Update(null,request.Name, request.Phone,null, request.UserId!);

        //Add Event

        //Save to Db - EfAutoSave will do this
        //await _repo.SaveAsync(cancellationToken);

        //Return result
        return _mapper.Map<ProfileBasicView>(profile);
    }
}