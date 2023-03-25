using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Core.Extensions;
using Module.Identity.Infrastructure.Extensions;
using IStartup = Module.Shared.IStartup;

namespace Module.Identity {
    public class Startup : IStartup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentityCore();
            services.AddIdentityInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/TestEndpoint",
                    async context => {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Identity Module");
                    }).RequireAuthorization()
            );
        }
    }
}