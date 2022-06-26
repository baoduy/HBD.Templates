using MediatR.AppServices.Share;
using MediatR.AppServices.Share.Exceptions;
using MediatR.Domains.Features.Profiles.Repos;

namespace MediatR.AppServices.Features.Profiles.Actions;

public class DeleteProfileCommand : BaseCommand, IRequest
{
    public Guid Id { get; set; }
}

internal sealed class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand>
{
    private readonly IProfileRepo _repository;

    public DeleteProfileCommandHandler(IProfileRepo repository) => _repository = repository;


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