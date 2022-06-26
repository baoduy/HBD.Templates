using HBD.EfCore.Repos;
using HBD.EfCore.Repos.Basic;
using MediatR.Domains.Features.Profiles.Repos;
using Microsoft.EntityFrameworkCore;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.Infra.Features.Profiles.Repos;

internal class ProfileRepo : Repository<Profile>, IProfileRepo
{
    public ProfileRepo(IBasicRepository repository) : base(repository)
    {
    }


    public Task<bool> IsEmailExistAsync(string email) => Get().AnyAsync(f => f.Email == email);
}