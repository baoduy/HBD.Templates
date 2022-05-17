using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TEMP.Api.Configs
{
    internal static class ApiConfig
    {
        #region Methods

        public static IServiceCollection AddApiConfig(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddMvcCore(op =>
                {
                    //var policy = new AuthorizationPolicyBuilder()
                    //      .RequireAuthenticatedUser()
                    //      .Build();
                    //op.Filters.Add(new AuthorizeFilter(policy));

                    //Requires Https in PRD. TODO: This is require if your API will be accessible directly from internet without proxy
                    // if (env.IsProduction())
                    //     op.Filters.Add<RequireHttpsAttribute>();

                    //op.AddModelHandlers();
                })
                .AddJsonOptions(op => { op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
                .AddApiExplorer()
                .AddAuthorization();

            services.AddControllers();

            return services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TODO: This is require if your API will be accessible directly from internet without proxy
            // if (env.IsProduction())
            //     app.UseHsts()
            //         .UseHttpsRedirection();

            app.UseRouting()
                .UseAuth()
                .UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            return app;
        }

        #endregion Methods
    }
}