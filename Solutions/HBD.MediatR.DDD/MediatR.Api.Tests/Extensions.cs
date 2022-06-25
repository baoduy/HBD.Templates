using HBD.Web.GlobalException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MediatR.Api.Tests;

public static class Extensions
{
    public static async Task<(bool success,TValue? result, ProblemDetails? error)> As<TValue>(this HttpResponseMessage message) where TValue : class
    {
        var success = message.IsSuccessStatusCode;
        TValue? result = default;
        ProblemDetails? error = default;
        
        var str = await message.Content.ReadAsStringAsync();
        if (success)
            result = JsonSerializer.Deserialize<TValue>(str);
        else error = JsonSerializer.Deserialize<ProblemDetails>(str);

        return (success,result, error);
    }
}