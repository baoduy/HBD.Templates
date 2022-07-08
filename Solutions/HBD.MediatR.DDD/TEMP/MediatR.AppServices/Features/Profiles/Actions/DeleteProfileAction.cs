using HBD.MediatR.DDD;
using HBD.Results;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Repos;

namespace MediatR.AppServices.Features.Profiles.Actions;

public class DeleteProfileCommand : BaseCommand, IRequestFluent
{
    public Guid Id { get; set; }
}

internal sealed class DeleteProfileCommandHandler : IRequestFluentHandler<DeleteProfileCommand>
{
    private readonly IProfileRepo _repository;

    public DeleteProfileCommandHandler(IProfileRepo repository) => _repository = repository;


    public async Task<IResult> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == default)
            return Result.Fails("The Id is in valid.", new[] { nameof(request.Id) });

        var profile = await _repository.FindAsync(request.Id);

        if (profile == null)
            return Result.Fails($"The Profile {request.Id} is not found.", new[] { nameof(request.Id) });

        _repository.Delete(profile);
        //EfAutoSave will do this
        //await _repository.SaveAsync(cancellationToken);

        //Event
        return Result.Ok();
    }
}