using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Host.Extensions {
    public static class ConfigureServiceContainer {
        public static void AddAutoMapper(this IServiceCollection serviceCollection) {
            var mappingConfig = new MapperConfiguration(cfg =>
                    cfg.AddMaps(new[] {
                        "Module.Identity.Core",
                    })
                );
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
