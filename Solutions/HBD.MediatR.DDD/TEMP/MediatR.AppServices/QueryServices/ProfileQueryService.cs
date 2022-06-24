using AutoMapper;
using HBD.EfCore.Repos;
using MediatR.AppServices.Models.Profiles;
using Microsoft.EntityFrameworkCore;
using Profile = MediatR.Domains.Aggregators.Profile;

namespace MediatR.AppServices.QueryServices;

internal sealed class ProfileQueryService : QueryService, IProfileQueryService
{
    private readonly IDtoRepository<Profile> _repository;

    public async ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId) 
        => await _repository.Get<ProfileBasicView>(p => p.AdAccountId == userId).FirstOrDefaultAsync();

    public async ValueTask<List<ProfileBasicView>> GetBasicViewAsync()
        => await _repository.Get<ProfileBasicView>().ToListAsync();

    public ProfileQueryService(IMapper mapper, IDtoRepository<Profile> repository) : base(mapper) => _repository = repository;
}