using HBD.EfCore.Repos;
using HBD.EfCore.Repos.Basic;
using MediatR.Domains.Repositories;
using Profile = MediatR.Domains.Aggregators.Profile;

namespace MediatR.Infra.Repos;

internal class ProfileRepo : Repository<Profile>, IProfileRepo
{
    public ProfileRepo(IBasicRepository repository) : base(repository)
    {
    }
}