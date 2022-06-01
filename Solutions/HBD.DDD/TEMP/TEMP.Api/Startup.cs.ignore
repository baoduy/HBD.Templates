using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using TEMP.Api.Configs;
// ReSharper disable CA1822

namespace TEMP.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        
        #region Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseGlobalExceptionHandler();

            //This will auto refresh the configuration from Azure App Configuration.
            if (Program.EnabledAzureAppConfiguration)
                app.UseAzureAppConfiguration();
            
            app.UseCrosConfig()
                .UseRouting()
                .UseHealthChecks(env)
                .UseApi(env);

            //app.UseSwaggerAndUI(provider);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCrosConfig(_configuration);

            // Service Layers
            services
                //.AddAppInsightsLog()
                .AddHealthCheckConfigs()
                .AddAllAppServices(_configuration)
                //.AddJwtAuth(_configuration, "TEMP.API")
                .AddApiConfig(_env);

            // Api Layer
            // services
            //     .AddVersioning()
            //     .AddSwaggerConfig(
            //         new SwaggerInfo
            //             {Title = $"TEMP Api ({_env.EnvironmentName})", Description = "The Api of TEMP Application."},
            //         builder =>
            //         {
            //             builder
            //                 .AddSecurityDefinition("Bearer", "Authorization");
            //         }, genOption:op =>
            //         {
            //             op.EnableAnnotations(true,true);
            //             op.CustomSchemaIds(i=>i.FullName);
            //         });

            // Feature Management with Azure App Configuration
            services.AddFeatureManagement();
            //This will auto refresh the configuration from Azure App Configuration.
            if (Program.EnabledAzureAppConfiguration)
                services.AddAzureAppConfiguration();
        }

        #endregion Methods
    }
}