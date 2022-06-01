using System;
using System.Threading.Tasks;
using TEMP.AppServices.Models.Profiles;

namespace TEMP.AppServices.QueryServices;

public interface IProfileQueryService : IQueryService
{
    ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId);
        
}