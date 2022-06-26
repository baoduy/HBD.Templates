using HBD.EfCore.Repos;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.Domains.Features.Profiles.Repos;

public interface IProfileRepo : IRepository<Profile>
{
    Task<bool> IsEmailExistAsync(string email);
}