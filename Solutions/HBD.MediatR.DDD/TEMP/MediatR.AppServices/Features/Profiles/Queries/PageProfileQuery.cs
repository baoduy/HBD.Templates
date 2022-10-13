using HBDStack.EfCore.Abstractions.Pageable;
using HBDStack.EfCore.Abstractions.QueryBuilders;
using HBDStack.EfCore.Repos.Abstractions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.AppServices.Features.Profiles.Queries;

public record PageProfileQuery : PageableQuery, IRequest<IPageable<ProfileBasicView>>
{
}

internal sealed class PageProfileQueryHandler : IRequestHandler<PageProfileQuery, IPageable<ProfileBasicView>>
{
    private readonly IDtoRepository<Profile> _repo;

    public PageProfileQueryHandler(IDtoRepository<Profile> repo) => _repo = repo;

    public async Task<IPageable<ProfileBasicView>> Handle(PageProfileQuery request, CancellationToken cancellationToken) =>
        await _repo.PageAsync(request.PageIndex, request.PageSize,
            OrderBuilder.CreateBuilder<ProfileBasicView>(i => i.Id), cancellationToken: cancellationToken);
}