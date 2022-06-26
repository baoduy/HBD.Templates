using System.Net.Http.Json;
using FluentAssertions;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Events;
using MediatR.AppServices.Features.Profiles.Models;

namespace MediatR.Api.Tests;

public class ProfileTests : IClassFixture<ApiFixture>
{
    private readonly ApiFixture _fixture;

    public ProfileTests(ApiFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_Profile()
    {
        var client = _fixture.CreateClient();
        ProfileCreatedEventHandler.Called = false;
        
        var rp = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "abc@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });

        var (success, result, error) = await rp.As<ProfileBasicView>();

        success.Should().BeTrue(error?.ErrorMessage);
        result.Should().NotBeNull();
        ProfileCreatedEventHandler.Called.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Duplicate_Profile()
    {
        var client = _fixture.CreateClient();
        //Create Profile
        await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "abc1@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });
        //And create other with the same email
        var rp = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "abc1@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });

        var (success, result, error) = await rp.As<ProfileBasicView>();

        success.Should().BeFalse();
        error.Should().NotBeNull();
        error.ErrorDetails[nameof(CreateProfileCommand.Email)].Should().NotBeNullOrEmpty();
    }
}