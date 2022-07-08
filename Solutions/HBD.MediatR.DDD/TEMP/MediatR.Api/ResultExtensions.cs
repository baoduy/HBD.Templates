using System.Diagnostics;
using System.Net;
using HBD.Results;
using HBD.Web.GlobalException;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;
using ProblemDetails = HBD.Web.GlobalException.ProblemDetails;

namespace MediatR.Api;

public static class ResultExtensions
{
    public static ProblemResultCollection Add(this ProblemResultCollection collection, IError error)
    {
        collection.Add(new ProblemResult(error.Code ?? string.Empty, error.Message)
        {
            References = error.MetaData
        });
        return collection;
    }

    public static ProblemDetails? ToProblemDetails(this HBD.Results.IResult result)
    {
        if (result.IsSuccess) return null;

        var problem = new ProblemDetails
        {
            ErrorMessage = "Something went wrong!",
            Status = HttpStatusCode.BadRequest,
            TraceId = Activity.Current?.RootId ?? Activity.Current?.Id,
        };

        if (result.Errors.Count == 1)
        {
            problem.ErrorMessage = result.Errors[0].Message;
            return problem;
        }

        foreach (var m in result.Errors)
            problem.ErrorDetails.Add(m);
        return problem;
    }

    public static IActionResult Send(this HBD.Results.IResult result)
        => result.IsSuccess ? new OkResult(): new BadRequestObjectResult(result.ToProblemDetails());

    public static ActionResult<TResponse?> Send<TResponse>(this IResult<TResponse> result)
        => result.IsSuccess ?new OkObjectResult(result.Value): new BadRequestObjectResult(result.ToProblemDetails());
}