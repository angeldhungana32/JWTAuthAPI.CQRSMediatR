using FluentValidation;
using JWTAuthAPI.API.AuthorizationHandlers;
using JWTAuthAPI.API.Validations;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace JWTAuthAPI.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions
                    .Converters.Add(new JsonStringEnumConverter()));

            services.AddEndpointsApiExplorer();
            services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();
            services.AddValidatorsFromAssemblyContaining<AuthenticateRequestValidator>();

            return services;
        }
    }
}
