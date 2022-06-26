using MediatR.AppServices.Features.Profiles.Models;

namespace MediatR.AppServices.Features.Profiles.Queries;

public interface IProfileQueryService : IQueryService
{
    ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId);
    ValueTask<List<ProfileBasicView>> GetBasicViewAsync();
        
}