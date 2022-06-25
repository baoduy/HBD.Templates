using System.Net.Http.Json;
using FluentAssertions;
using MediatR.AppServices.BizActions.Profiles;
using MediatR.AppServices.Models.Profiles;

namespace MediatR.Api.Tests;

public class ProfileTests : IClassFixture<ApiFixture>
{
    private readonly ApiFixture _fixture;

    public ProfileTests(ApiFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_Profile()
    {
        var client = _fixture.CreateClient();

        var rp = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "hbd@abc.com",
            Name = "HBD",
            Phone = "123456"
        });
        
        var (success, result, error) = await rp.As<ProfileBasicView>();

        success.Should().BeTrue();
        result.Should().NotBeNull();
    }
}