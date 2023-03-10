using JWTAuthAPI.API.Extensions;
using JWTAuthAPI.API.Services;
using JWTAuthAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace JWTAuthAPI.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(options => 
                options.JsonSerializerOptions
                   .Converters
                   .Add(new JsonStringEnumConverter()));

            services.AddEndpointsApiExplorer();

            services.AddHttpContextAccessor();
            services.AddSwaggerCustom(configuration);

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
