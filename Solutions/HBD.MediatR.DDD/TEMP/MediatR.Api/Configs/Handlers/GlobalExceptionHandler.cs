using System.Net;
using System.Text.Json;
using HBD.Web.GlobalException;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Api.Configs.Handlers;

internal class GlobalExceptionHandler : DefaultGlobalExceptionHandler
{
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : base(logger)
    {
    }

    public override async ValueTask<ProblemDetails> HandleExceptionAsync(Exception exception)
    {
        var problems = await base.HandleExceptionAsync(exception);

        switch (exception)
        {
            // case BizCommandException ex:
            //     problems.Status = HttpStatusCode.BadRequest;
            //     problems.ErrorDetails.Add(new GenericValidationResult(ex.Message, ex.Fields));
            //     break;
            case DbUpdateException ex:
                problems.Status = HttpStatusCode.BadRequest;
                problems.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                break;
        }

        return problems;
    }

    public override string Serialize(ProblemDetails? problemDetails) =>
        JsonSerializer.Serialize(problemDetails,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
}