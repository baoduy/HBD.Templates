using HBDStack.EfCore.Abstractions.Pageable;
using HBDStack.EfCore.Abstractions.QueryBuilders;
using HBDStack.EfCore.Repos.Abstractions;
using Mapster;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Share;
using MediatR.Domains.Features.Profiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.AppServices.Features.Profiles.Queries;

public record PageProfileQuery : PageableQuery, IRequest<IPageable<ProfileBasicView>>
{
}

internal sealed class PageProfileQueryHandler : IRequestHandler<PageProfileQuery, IPageable<ProfileBasicView>>
{
    private readonly IRepository<Profile> _repository;
    
    public PageProfileQueryHandler(IRepository<Profile> repository) => _repository = repository;

    public async Task<IPageable<ProfileBasicView>> Handle(PageProfileQuery request, CancellationToken cancellationToken) =>
        await _repository.Get().ProjectToType<ProfileBasicView>()
            .OrderBy(t=>t.Id)
            .ToPageableAsync(request.PageIndex, request.PageSize, cancellationToken: cancellationToken);
}