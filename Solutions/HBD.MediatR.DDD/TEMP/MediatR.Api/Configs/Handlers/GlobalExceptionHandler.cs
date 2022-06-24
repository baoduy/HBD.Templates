using HBD.Web.GlobalException;

namespace MediatR.Api.Configs.Handlers;

internal class GlobalExceptionHandler:DefaultGlobalExceptionHandler
{
    public GlobalExceptionHandler(ILogger<DefaultGlobalExceptionHandler> logger) : base(logger)
    {
    }

    public override ValueTask<ProblemDetails> HandleExceptionAsync(Exception exception)
    {
        return base.HandleExceptionAsync(exception);
    }
}