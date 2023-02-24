using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using JWTAuthAPI.Application.Common.Behaviors;
using MediatR;
using System.Reflection;
using JWTAuthAPI.Core.Constants;
using JWTAuthAPI.Core.AuthorizationRequirement;
using JWTAuthAPI.Application.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddAuthorizationCore(options =>
               options.AddPolicy("UserIsAdmin",
                   policy => policy.RequireRole(Roles.ADMIN)));

            services.AddAuthorizationCore(options =>
                options.AddPolicy("UserIsOwner", policy =>
                    policy.Requirements.Add(new UserIsOwnerRequirement())));

            return services;
        }
    }
}
