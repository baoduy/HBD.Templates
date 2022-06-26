using AutoMapper;
using HBD.EfCore.Repos;
using MediatR.AppServices.Features.Profiles.Models;
using Microsoft.EntityFrameworkCore;
using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;

namespace MediatR.AppServices.Features.Profiles.Queries;

internal sealed class ProfileQueryService : QueryService, IProfileQueryService
{
    private readonly IDtoRepository<Profile> _repository;

    public async ValueTask<ProfileBasicView> GetBasicViewForUserAsync(Guid userId) 
        => await _repository.Get<ProfileBasicView>(p => p.CreatedBy == userId.ToString()).FirstOrDefaultAsync();

    public async ValueTask<List<ProfileBasicView>> GetBasicViewAsync()
        => await _repository.Get<ProfileBasicView>().ToListAsync();

    public ProfileQueryService(IMapper mapper, IDtoRepository<Profile> repository) : base(mapper) => _repository = repository;
}