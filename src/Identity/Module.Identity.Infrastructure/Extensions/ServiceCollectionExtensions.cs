using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Core.Repositories;
using Module.Identity.Infrastructure.Repositories;

namespace Module.Identity.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services) {
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            return services;
        }
    }
}
