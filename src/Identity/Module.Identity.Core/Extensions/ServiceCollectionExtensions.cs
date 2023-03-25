using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Core.Contracts.Services;
using Module.Identity.Core.Services;

namespace Module.Identity.Core.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddIdentityCore(this IServiceCollection services) {
            services.AddTransient<IAccountService, AccountService>();
            return services;
        }
    }
}
