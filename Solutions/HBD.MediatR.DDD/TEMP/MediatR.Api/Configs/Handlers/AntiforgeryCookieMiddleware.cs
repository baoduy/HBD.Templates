using Microsoft.AspNetCore.Antiforgery;

namespace MediatR.Api.Configs.Handlers;

public class AntiforgeryCookieMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAntiforgery _antiforgery;
    private readonly string[] _validateMethods = { "POST", "PUT", "PATCH", "DELETE" };

    public AntiforgeryCookieMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
        _next = next;
        _antiforgery = antiforgery;
    }

    private AntiforgeryTokenSet GetCookieToken(HttpContext context)
    {
        context.Request.Cookies.TryGetValue(ServiceConfigs.CsrfHeaderKey, out var requestToken);
        context.Request.Cookies.TryGetValue(ServiceConfigs.CsrfCookieKey, out var cookieToken);

        return new AntiforgeryTokenSet(requestToken, cookieToken, ServiceConfigs.CsrfFieldKey,
            ServiceConfigs.CsrfHeaderKey);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Validation
        var method = context.Request.Method;

        AntiforgeryTokenSet token;
        if (_validateMethods.Contains(method, StringComparer.OrdinalIgnoreCase))
        {
            token = GetCookieToken(context);
            context.Request.Headers[ServiceConfigs.CsrfHeaderKey] = token.RequestToken;
        }
        else
        {
            //Generate Token
            token = _antiforgery.GetTokens(context);
            if (string.IsNullOrWhiteSpace(token.CookieToken))
            {
                var oldToken = GetCookieToken(context);
                token = new AntiforgeryTokenSet(token.RequestToken, oldToken.CookieToken, token.FormFieldName,
                    token.HeaderName);
            }
        }

        context.Response.Cookies.Append(ServiceConfigs.CsrfCookieKey, token.CookieToken ?? string.Empty);
        context.Response.Cookies.Append(ServiceConfigs.CsrfHeaderKey, token.RequestToken ?? string.Empty);

        await _next(context);
    }
}