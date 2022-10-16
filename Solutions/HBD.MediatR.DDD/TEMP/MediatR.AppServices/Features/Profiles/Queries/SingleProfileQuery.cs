using HBDStack.EfCore.Repos.Abstractions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.AppServices.Features.Profiles.Queries;

public record SingleProfileQuery : IRequest<ProfileBasicView?>
{
    public Guid Id { get; set; }
}

internal sealed class SingleProfileQueryHandler : IRequestHandler<SingleProfileQuery, ProfileBasicView?>
{
    private readonly IRepository<Profile> _repo;

    public SingleProfileQueryHandler(IRepository<Profile> repo) => _repo = repo;


    public async Task<ProfileBasicView?> Handle(SingleProfileQuery request, CancellationToken cancellationToken)
        => await _repo. FindAsync(p => p.Id == request.Id, cancellationToken);
}