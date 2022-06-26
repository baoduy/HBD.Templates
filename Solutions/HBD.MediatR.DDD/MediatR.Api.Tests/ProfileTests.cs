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
        ProfileCreatedEventAuditTrailHandler.Called = false;
        ProfileCreatedEventServiceBusHandler.Called = false;
        
        var client = _fixture.CreateClient();
        var rp = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "abc@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });

        var (success, result, error,_) = await rp.As<ProfileBasicView>();

        success.Should().BeTrue(error?.ErrorMessage);
        result.Should().NotBeNull();
        result!.Id.Should().NotBeEmpty();
        
        ProfileCreatedEventAuditTrailHandler.Called.Should().BeTrue();
        ProfileCreatedEventServiceBusHandler.Called.Should().BeTrue();
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

        var (success, result, error,content) = await rp.As<ProfileBasicView>();

        success.Should().BeFalse();
        error.Should().NotBeNull();
        error!.ErrorDetails[nameof(CreateProfileCommand.Email)].Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Update_Profile()
    {
        var client = _fixture.CreateClient();
        
        //Create Profile
        var created = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "update_test@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });
        
        var (_, createdResult, createdError,_) = await created.As<ProfileBasicView>();
        createdResult.Should().NotBeNull(createdError?.ErrorMessage);
        
        //Update
        var rp = await client.PutAsJsonAsync($"/v1/Profile/{createdResult!.Id}", new UpdateProfileCommand
        {
            Id = createdResult.Id,
            Name = "HBD New",
            Phone = "+6512399999"
        });

        var (success, result, error,_) = await rp.As<ProfileBasicView>();

        success.Should().BeTrue(error?.ErrorMessage);
        result.Should().NotBeNull();
        result!.Id.Should().NotBeEmpty();
    }
 
    [Fact]
    public async Task Delete_Profile()
    {
        var client = _fixture.CreateClient();
        
        //Create Profile
        var created = await client.PostAsJsonAsync("/v1/Profile", new CreateProfileCommand
        {
            Email = "delete_test@hbd.com",
            Name = "HBD",
            Phone = "+6512345678"
        });
        
        var (_, createdResult, createdError,_) = await created.As<ProfileBasicView>();
        createdResult.Should().NotBeNull(createdError?.ErrorMessage);
        
        //Delete
        var rp = await client.DeleteAsync($"/v1/Profile/{createdResult!.Id}");

        var (success, _, error,_) = await rp.As<ProfileBasicView>();
        success.Should().BeTrue(error?.ErrorMessage);
    }
}