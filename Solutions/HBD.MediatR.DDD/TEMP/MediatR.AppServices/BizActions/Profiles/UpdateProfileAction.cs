using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR.AppServices.Exceptions;
using MediatR.AppServices.Models.Profiles;
using MediatR.Domains.Repositories;
using Profile = MediatR.Domains.Aggregators.Profile;

namespace MediatR.AppServices.BizActions.Profiles;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class UpdateProfileCommand : CreateProfileCommand
{
    [Required]
    public Guid Id { get; set; }
}

internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileBasicView>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repo;

    public UpdateProfileCommandHandler(IMediator mediator,
        IMapper mapper,
        IProfileRepo repo)
    {
        _mediator = mediator;
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
        profile.UpdateName(request.Name, request.UserId);
        await _repo.SaveAsync(cancellationToken);
        
        //Event
        
        return _mapper.Map<ProfileBasicView>(profile);
    }
}