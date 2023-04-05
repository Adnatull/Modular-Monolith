using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Module.Host.Extensions;
using Module.Host.Middleware;
using Module.Host.Permissions;
using Module.Shared.Filters;
using Module.Shared.Permissions;
using Serilog;
using System;
using System.Collections.Generic;
using IdentityModule = Module.Identity;

namespace Module.Host {
    public class Startup
    {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config => {
                // Customizing modelState validation response
                config.Filters.Add(new GlobalValidationFilter());

            }).ConfigureApplicationPartManager(manager => {
                // Clear all auto detected controllers.
                manager.ApplicationParts.Clear();

                // Add feature provider to allow "internal" controller
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            // Register a convention allowing to us to prefix routes to modules.
            services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();

            // Adds module2 with the route prefix identity
            services.AddModule<IdentityModule.Startup>("identity", Configuration);       


            services.AddSwagger();


            services.AddSingleton<IPermissionHelper, PermissionHelper>();
            services.AddAutoMapper();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Disabling Default API ModelState Validation Filter
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = String.Empty;
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "Modular Monolithic V1");
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ApiErrorHandlerMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // Adds endpoints defined in modules
            var modules = app.ApplicationServices.GetRequiredService<IEnumerable<Module>>();
            foreach (var module in modules)
            {
                app.Map($"/{module.RoutePrefix}", builder =>
                {
                    builder.UseRouting();
                    module.Startup.Configure(builder, env);
                });
            }            
        }        
    }
}