using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

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

        public static void AddSwagger(this IServiceCollection serviceCollection) {
            serviceCollection.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo() {
                    Version = "v1",
                    Title = "Modular Monolithic"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
