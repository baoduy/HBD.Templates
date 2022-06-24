using AutoMapper;
using HBD.EfCore.Repos;
using MediatR.AppServices.Exceptions;
using MediatR.AppServices.Models.Profiles;
using MediatR.Domains.Repositories;
using Profile = MediatR.Domains.Aggregators.Profile;


namespace MediatR.AppServices.BizActions.Profiles;

public class DeleteProfileCommand:IRequest
{
    public Guid Id { get; set; }
}

internal class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand>
{
    private readonly IMediator _mediator;
    private readonly IProfileRepo _repository;
    private readonly IMapper _mapper;

    public DeleteProfileCommandHandler( IMediator mediator, 
        IProfileRepo repository,
        IMapper mapper)
    {
        _mediator = mediator;
        _repository = repository;
        _mapper = mapper;
    }
    

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == default)
            throw new BizCommandException("The Id is in valid.", nameof(request.Id));

        var profile = await _repository.FindAsync(request.Id);

        if (profile == null)
            throw new BizCommandException($"The Profile {request.Id} is not found.", nameof(request.Id));

        _repository.Delete(profile);
        await _repository.SaveAsync(cancellationToken);

        //Event
        return Unit.Value;
    }
}