using HBD.EfCore.Extensions.Configurations;
using MediatR.Core;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.Infra.Features.Profiles.StaticData;

internal class ProfileData : IDataSeedingConfiguration<Profile>
{
    public ICollection<Profile> Data => new List<Profile>
    {
        new(new Guid("A6B50327-160E-423C-9C0B-C125588E6025"), "Steven Hoang", "MS12345",
            "abc@gmail.com", "123456789", SysConsts.SystemAccount)
    };
}