using HBD.Web.Swagger;
using MediatR.Api.Configs.Handlers;
using MediatR.Core.Options;

namespace MediatR.Api.Configs;

internal static class ApiConfig
{
    public static WebApplication EnableFeatures(this WebApplication app, IConfiguration configuration)
    {
        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);

        if (features.EnableHttps)
            app.UseHsts().UseHttpsRedirection();

        if (features.EnableSwagger)
            app.UseSwaggerAndUI();

        app.UseCors();

        return app;
    }

    public static WebApplication EnableDevFeatures(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;

        app.UseDeveloperExceptionPage();
        return app;
    }

    public static WebApplication UseAuthentications(this WebApplication app, IConfiguration configuration)
    {
        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);

        app.UseCookiePolicy();

        if (features.RequireAuthorization)
            app.UseAuthentication();

        app.UseAuthorization();
        return app;
    }

    public static WebApplication UseMiddlewares(this WebApplication app, IConfiguration configuration)
    {
        app.UseGlobalExceptionHandler<GlobalExceptionHandler>();

        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);
        if (features.EnableAntiforgery)
            app.UseMiddleware<AntiforgeryCookieMiddleware>();

        return app;
    }

    public static WebApplication UseEndpointsWithHealthCheck(this WebApplication app)
    {
        app.UseEndpoints(endpoint => endpoint.MapHealthzCheck());
        return app;
    }

    // public static WebApplication UseHangfireUI(this WebApplication app)
    // {
    //     var features = app.Configuration.GetBindValue<FeatureOptions>(FeatureOptions.Name);
    //     if (!features.EnableHangfire) return app;
    //
    //     var options = new DashboardOptions { IsReadOnlyFunc = _ => false };
    //     if (features.RequireAuthorization)
    //         options.Authorization = new[] { new DashboardAuthorizationFilter() };
    //
    //     app.UseHangfireDashboard(options: options);
    //
    //     return app;
    // } 
}