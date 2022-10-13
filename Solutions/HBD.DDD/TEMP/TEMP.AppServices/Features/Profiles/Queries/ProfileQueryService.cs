using AutoMapper;
using HBDStack.EfCore.Abstractions.Pageable;
using HBDStack.EfCore.Abstractions.QueryBuilders;
using HBDStack.EfCore.Repos.Abstractions;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Share;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.AppServices.Features.Profiles.Queries;

internal sealed class ProfileQueryService : QueryService, IProfileQueryService
{
    private readonly IDtoRepository<Profile> _repository;

    public async ValueTask<ProfileBasicView?> GetById(Guid id) 
        => await _repository.FindAsync<ProfileBasicView>(p => p.Id == id);

    public async ValueTask<IPageable<ProfileBasicView>> GetPages(PageableQueryModel queryModel) =>
        await _repository.PageAsync(queryModel.PageIndex, queryModel.PageSize,
            OrderBuilder.CreateBuilder<ProfileBasicView>(i => i.Id));

    public ProfileQueryService(IMapper mapper, IDtoRepository<Profile> repository) : base(mapper) => _repository = repository;
}