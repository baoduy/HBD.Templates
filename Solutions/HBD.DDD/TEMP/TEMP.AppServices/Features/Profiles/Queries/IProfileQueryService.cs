using HBD.EfCore.Abstractions.Pageable;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Share;

namespace TEMP.AppServices.Features.Profiles.Queries;

public interface IProfileQueryService : IQueryService
{
    ValueTask<ProfileBasicView?> GetById(Guid userId);
    ValueTask<IPageable<ProfileBasicView>> GetPages(PageableQueryModel queryModel);
        
}