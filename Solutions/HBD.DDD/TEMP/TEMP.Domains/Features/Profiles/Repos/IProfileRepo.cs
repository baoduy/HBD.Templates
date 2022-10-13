using HBDStack.EfCore.Repos.Abstractions;
using TEMP.Domains.Features.Profiles.Entities;

namespace TEMP.Domains.Features.Profiles.Repos;

public interface IProfileRepo : IRepository<Profile>
{
    Task<bool> IsEmailExistAsync(string email);
}