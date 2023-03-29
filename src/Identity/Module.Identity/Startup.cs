using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Module.Identity.Core.Entities;
using Module.Identity.Core.Extensions;
using Module.Identity.Infrastructure.Context;
using Module.Identity.Infrastructure.Extensions;
using System.Text;
using IStartup = Module.Shared.IStartup;

namespace Module.Identity {
    public class Startup : IStartup {

        
        private IConfiguration? Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;

            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options => {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Audience"],
                       IssuerSigningKey = new
                       SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes
                       (Configuration["Jwt:Key"]))
                   };
               });


            services.AddIdentityCore();
            services.AddIdentityInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/TestEndpoint",
                    async context => {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Identity Module");
                    }).RequireAuthorization()
            );
        }

        public void ConfigureConstructor(IConfiguration configuration) {
            Configuration = configuration;
        }
    }
}