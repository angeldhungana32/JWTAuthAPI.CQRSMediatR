using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Constants;
using JWTAuthAPI.Core.Interfaces;
using JWTAuthAPI.Infrastructure.Services;
using JWTAuthAPI.Infrastructure.Data;
using JWTAuthAPI.Infrastructure.Extensions;
using JWTAuthAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTAuthAPI.Infrastructure.Configurations;

namespace JWTAuthAPI.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>(ConfigurationSectionKeyConstants.UseInMemoryDB))
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseInMemoryDatabase(ConfigurationSectionKeyConstants.DBName));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString(ConfigurationSectionKeyConstants.DBConnectionString)));
            }

            services.AddIdentityCore<ApplicationUser>(
                options =>
                {
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.User.RequireUniqueEmail = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            
            services.Configure<AdminConfiguration>(configuration.GetSection(ConfigurationSectionKeyConstants.Admin));
            services.Configure<RoleConfiguration>(configuration.GetSection(ConfigurationSectionKeyConstants.Roles));

            services.AddTransient<ApplicationDbInitializer>();
            services.AddTransient<IRepositoryActivator, RepositoryActivator>();

            services.AddScoped<ITokenService, JwtTokenService>();

            services.AddJwtAuthentication(configuration);

            services.AddAuthorization(options => options.AddPolicy(AuthorizationPolicies.UserIsAdminPolicy,
                   policy => policy.RequireRole(Roles.ADMIN)));

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IResourceAuthorizationService, ResourceAuthorizationService>();

            return services;
        }
    }
}
