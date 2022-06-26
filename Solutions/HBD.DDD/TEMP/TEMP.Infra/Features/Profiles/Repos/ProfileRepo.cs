using HBD.EfCore.Repos;
using HBD.EfCore.Repos.Basic;
using Microsoft.EntityFrameworkCore;
using TEMP.Domains.Features.Profiles.Entities;
using TEMP.Domains.Features.Profiles.Repos;

namespace TEMP.Infra.Features.Profiles.Repos;

internal class ProfileRepo : Repository<Profile>, IProfileRepo
{
    public ProfileRepo(IBasicRepository repository) : base(repository)
    {
    }
    
    public Task<bool> IsEmailExistAsync(string email) => Get().AnyAsync(f => f.Email == email);
}