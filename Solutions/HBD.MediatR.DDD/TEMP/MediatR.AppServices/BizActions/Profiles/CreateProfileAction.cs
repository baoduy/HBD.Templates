using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR.AppServices.Models.Profiles;
using MediatR.Domains.Events;
using MediatR.Domains.Repositories;
using MediatR.Domains.Services;
using Profile = MediatR.Domains.Aggregators.Profile;

namespace MediatR.AppServices.BizActions.Profiles;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class CreateProfileCommand :BaseCommand, IRequest<ProfileBasicView>
{
    [Required] public string Email { get; set; }
    
    [Phone] public string Phone { get; set; }

    internal Guid AdAccountId { get; set; }

    internal string MembershipNo { get; set; }

    [StringLength(150)] [Required] public string Name { get; set; }
}

internal sealed class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, ProfileBasicView>
{
    private readonly IMediator _mediator;
    private readonly IMembershipService _membershipProvider;
    private readonly IMapper _mapper;
    private readonly IProfileRepo _repository;

    public CreateProfileCommandHandler(
        IMediator mediator, 
        IProfileRepo repository,
        IMembershipService membershipProvider,
        IMapper mapper)
    {
        _mediator = mediator;
        _membershipProvider = membershipProvider;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProfileBasicView> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.MembershipNo))
            request.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

        var profile = new Profile(request.Name,
            request.MembershipNo,
            request.AdAccountId,
            request.Email,
            request.Phone,
            request.UserId);

        //Event
        profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

        await _repository.AddAsync(profile, cancellationToken);
        await _repository.SaveAsync(cancellationToken);
        
        //Event
        return _mapper.Map<ProfileBasicView>(profile);
    }
}