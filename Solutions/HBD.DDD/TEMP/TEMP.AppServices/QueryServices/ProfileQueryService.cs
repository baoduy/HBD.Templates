using System;
using System.Threading.Tasks;
using AutoMapper;
using HBD.EfCore.Repos;
using Microsoft.EntityFrameworkCore;
using TEMP.AppServices.Models.Profiles;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.AppServices.QueryServices
{
    internal sealed class ProfileQueryService : QueryService, IProfileQueryService
    {
        private readonly IDtoRepository<Profile> _repository;

        public async ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId) 
            => await _repository.Get<ProfileBasicView>(p => p.AdAccountId == userId).FirstOrDefaultAsync();

        public ProfileQueryService(IMapper mapper, IDtoRepository<Profile> repository) : base(mapper) => _repository = repository;
    }
}