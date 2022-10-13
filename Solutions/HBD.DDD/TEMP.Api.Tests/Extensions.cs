using System.Text.Json;
using HBDStack.Web.GlobalException;

namespace TEMP.Api.Tests;

public static class Extensions
{
    private static JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task<(bool success, TValue? result, ProblemDetails? error, string? content)> As<TValue>(
        this HttpResponseMessage message) where TValue : class
    {
        var success = message.IsSuccessStatusCode;
        TValue? result = default;
        ProblemDetails? error = default;

        var str = await message.Content.ReadAsStringAsync();
        
        if (!string.IsNullOrEmpty(str))
        {
            if (success)
                result = JsonSerializer.Deserialize<TValue>(str, _options);
            else error = JsonSerializer.Deserialize<ProblemDetails>(str, _options);
        }

        return (success, result, error, str);
    }
}