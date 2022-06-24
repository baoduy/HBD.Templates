using System.Text.Json;
using System.Text.Json.Serialization;
using HBD.EfCore.DataAuthorization;
using HBD.Web.GlobalException;
using HBD.Web.Swagger;
using MediatR.Api.Configs.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.FeatureManagement;
using MediatR.AppServices;
using MediatR.Core;
using MediatR.Core.Options;
using MediatR.Domains;
using MediatR.Infra;

namespace MediatR.Api.Configs;

internal static class ServiceConfigs
{
    //public const string CorsName = $"{AppName}-CORS";
    public const string CookieHeaderKey = "SET-COOKIE";
    public const string CsrfHeaderKey = $"{SettingKeys.ApiName}-CSRF-TOKEN";
    public const string CsrfCookieKey = $"X-{CsrfHeaderKey}";
    public const string CsrfFieldKey = $"__{CsrfHeaderKey}";

    public static IServiceCollection AddAspNetConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement();

        //TODO move to external cache
        services.AddDistributedMemoryCache();

        //Antiforgery
        var features = configuration.Bind<FeatureOptions>(FeatureOptions.Name);
        if (features.EnableAntiforgery)
        {
            services.AddAntiforgery(config =>
            {
                config.Cookie.Name = CsrfCookieKey;
                config.HeaderName = CsrfHeaderKey;
                config.FormFieldName = CsrfFieldKey;

                config.Cookie.HttpOnly = true;
                config.Cookie.SameSite = SameSiteMode.Strict;
                config.Cookie.SecurePolicy =
                    features.EnableHttps ? CookieSecurePolicy.Always : CookieSecurePolicy.SameAsRequest;
                config.SuppressXFrameOptionsHeader = false;
            });
        }

        //Cors
        services.AddCors(c => c.AddDefaultPolicy(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        services
            .AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            })
            .AddControllers(
                config =>
                {
                    if (features.RequireAuthorization)
                    {
                        config.Filters.Add(new AuthorizeFilter());
                        //config.Filters.Add<SetActionUserForModelFilter>(2);
                    }

                    if (features.EnableAntiforgery)
                        config.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                })
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new BadRequestObjectResult(context.ModelState.ToProblemDetails());
            });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerConfig(new SwaggerInfo
        {
            Title = SettingKeys.ApiName,
            Description = $"The {SettingKeys.ApiName} Api documentation."
        }, xmlFile: $"{SettingKeys.ApiName}.xml");

        return services;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FeatureOptions>(configuration.GetSection(FeatureOptions.Name));
        return services;
    }

    public static IServiceCollection AddAllAppServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddScoped<IPrincipalProvider, PrincipalProvider>()
            .AddScoped<IDataKeyProvider>(p => p.GetRequiredService<IPrincipalProvider>());

        var conn = configuration.GetConnectionString(SettingKeys.DbConnectionString);

        services.AddAutoMapper(cfg =>
            {
                cfg.ShouldUseConstructor = c => c.IsPublic;
                cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.CanRead;
            }, typeof(AppSetup).Assembly, typeof(DomainSchemas).Assembly
            /*typeof(AuthSetup).Assembly*/
        );
        
        return services
            .AddAppServices()
            .AddInfraServices(conn)
            //Service Bus
            .AddInfraServiceBus(configuration);
    }
}