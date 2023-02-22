using JWTAuthAPI.API.Services;
using JWTAuthAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace JWTAuthAPI.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(options => 
                options.JsonSerializerOptions
                   .Converters
                   .Add(new JsonStringEnumConverter()));

            services.AddEndpointsApiExplorer();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


            return services;
        }
    }
}
