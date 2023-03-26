using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Core.Entities;
using Module.Identity.Core.Repositories;
using Module.Identity.Infrastructure.Context;
using Module.Identity.Infrastructure.Repositories;

namespace Module.Identity.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration) {


            services.AddDbContext<IdentityContext>(options =>
                                    options.UseSqlServer(
                                            Environment.GetEnvironmentVariable("IdentityConnection") ??
                                            configuration.GetConnectionString("IdentityConnection"),
                                            b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));


            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();            

            return services;
        }
    }
}
