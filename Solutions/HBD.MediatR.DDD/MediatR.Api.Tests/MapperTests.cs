using FluentAssertions;
using Mapster;
using MediatR.AppServices.Models;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.Api.Tests;

public class MapperTests
{
    [Fact]
    public void Create_Profile()
    {
        TypeAdapterConfig.GlobalSettings.ForType<ProfileDto,Profile>()
            .MapToConstructor(true)
            .ConstructUsing(s=> new Profile(s.Name,s.MembershipNo,s.Email,s.Phone,s.CreatedBy))
            .PreserveReference(true);
            
        var rs = new ProfileDto
        {
            Id = Guid.NewGuid(),
            Name = "Steven",
            CreatedBy = "Steven"
        }.Adapt<Profile>();

        rs.Name.Should().Be("Steven");
        
        rs.Adapt<ProfileDto>().Name.Should().Be("Steven");
    }
}