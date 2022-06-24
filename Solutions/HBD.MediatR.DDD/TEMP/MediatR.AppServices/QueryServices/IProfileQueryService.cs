using MediatR.AppServices.Models.Profiles;

namespace MediatR.AppServices.QueryServices;

public interface IProfileQueryService : IQueryService
{
    ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId);
    ValueTask<List<ProfileBasicView>> GetBasicViewAsync();
        
}