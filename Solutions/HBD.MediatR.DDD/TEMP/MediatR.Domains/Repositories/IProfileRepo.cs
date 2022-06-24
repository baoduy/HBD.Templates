using HBD.EfCore.Repos;
using MediatR.Domains.Aggregators;

namespace MediatR.Domains.Repositories;

public interface IProfileRepo : IRepository<Profile>
{
}