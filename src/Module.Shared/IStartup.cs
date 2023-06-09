﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Shared
{
    public interface IStartup
    {
        void ConfigureConstructor(IConfiguration configuration);
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}