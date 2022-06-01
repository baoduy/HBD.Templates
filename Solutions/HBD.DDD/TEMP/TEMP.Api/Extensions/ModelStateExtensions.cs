using System.Diagnostics;
using System.Net;
using HBD.StatusGeneric;
using HBD.Web.GlobalException;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TEMP.Api.Extensions;

public static class ModelStateExtensions
{
    public static ProblemDetails ToProblemDetails(this ModelStateDictionary validationException)
    {
        var errorCollection = new ProblemResultCollection();
        foreach (var (key, value) in validationException)
        {
            errorCollection.AddRange(value.Errors.Select(i =>
                new GenericValidationResult("invalid_argument", i.ErrorMessage, new[] { key })));
        }

        return new ProblemDetails
        {
            Status = HttpStatusCode.BadRequest,
            ErrorDetails = errorCollection,
            ErrorMessage = "One or more validation errors occurred.",
            TraceId = Activity.Current?.RootId ?? Activity.Current?.TraceId.ToString() ?? null
        };
    }
}