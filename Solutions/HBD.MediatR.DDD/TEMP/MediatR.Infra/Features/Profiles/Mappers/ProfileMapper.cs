using MediatR.Domains.Features.Profiles.Entities;
using MediatR.Infra.Share;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatR.Infra.Features.Profiles.Mappers;

internal sealed class ProfileMapper : DefaultMapper<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(builder);

        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasIndex(p => p.MembershipNo).IsUnique();
    }
}