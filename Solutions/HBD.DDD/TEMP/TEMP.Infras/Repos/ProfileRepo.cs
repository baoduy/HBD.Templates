using HBD.EfCore.Repos;
using HBD.EfCore.Repos.Basic;
using TEMP.Domains.Repositories;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.Infras.Repos
{
    internal class ProfileRepo : Repository<Profile>, IProfileRepo
    {
        public ProfileRepo(IBasicRepository repository) : base(repository)
        {
        }
    }
}