using System.Diagnostics;
using System.Net;
using FluentResults;
using HBD.StatusGeneric;
using HBD.Web.GlobalException;
using Microsoft.AspNetCore.Mvc;
using ProblemDetails = HBD.Web.GlobalException.ProblemDetails;

namespace MediatR.Api;

public static class ResultExtensions
{
    public static void Add(this ProblemResultCollection collection, IError error)
    {
        if (error.Metadata.Any())
            foreach (var m in error.Metadata)
                collection.Add(m.Key, m.Value.ToString());
        else collection.Add(string.Empty, error.Message);
    }

    public static ProblemDetails? ToProblemDetails(this IResultBase result)
    {
        if (result.IsSuccess) return null;

        var problem = new ProblemDetails
        {
            ErrorMessage = "Something went wrong!",
            Status = HttpStatusCode.BadRequest,
            TraceId = Activity.Current?.RootId ?? Activity.Current?.Id,
        };

        if (result.Errors.Count == 1 && !result.Errors.First().Metadata.Any())
        {
            problem.ErrorMessage = result.Errors.First().Message;
            return problem;
        }

        foreach (var m in result.Errors)
            problem.ErrorDetails.Add(m);
        return problem;
    }

    public static IActionResult Send(this Result result)
        => result.IsFailed ? new BadRequestObjectResult(result.ToProblemDetails()) : new OkResult();

    public static ActionResult<TResponse?> Send<TResponse>(this IResult<TResponse> result)
        => result.IsFailed ? new BadRequestObjectResult(result.ToProblemDetails()) : new OkObjectResult(result.Value);
}