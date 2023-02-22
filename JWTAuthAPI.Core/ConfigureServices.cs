﻿using JWTAuthAPI.Core.Interfaces;
using JWTAuthAPI.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthAPI.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
