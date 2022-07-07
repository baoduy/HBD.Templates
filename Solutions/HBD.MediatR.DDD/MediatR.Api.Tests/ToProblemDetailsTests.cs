using System.Net;
using System.Text.Json;
using FluentAssertions;
using FluentResults;
using MediatR.AppServices.Share;
using Xunit.Abstractions;

namespace MediatR.Api.Tests;

public class ToProblemDetailsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ToProblemDetailsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ToProblems_Null()
    {
        var rs = Result.Ok("The id is invalid.").ToProblemDetails();
        rs.Should().BeNull();
    }

    [Fact]
    public void ToProblems()
    {
        var rs = Result.Fail("The id is invalid.").ToProblemDetails();
        rs!.Status.Should().Be(HttpStatusCode.BadRequest);
        rs.ErrorMessage.Should().Be("The id is invalid.");

        _testOutputHelper.WriteLine(JsonSerializer.Serialize(rs, new JsonSerializerOptions { WriteIndented = true }));
    }

    [Fact]
    public void ToProblems_WithDetails()
    {
        var rs = Result.Fail("The are many errors.")
            .WithError(new BizCommandError("bad code", "Error1"))
            .WithError("Stupid code")
            .ToProblemDetails();

        rs!.Status.Should().Be(HttpStatusCode.BadRequest);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(rs, new JsonSerializerOptions { WriteIndented = true }));
    }
}