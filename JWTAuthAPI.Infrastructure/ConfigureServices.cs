using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Constants;
using JWTAuthAPI.Core.Interfaces;
using JWTAuthAPI.Core.Services;
using JWTAuthAPI.Infrastructure.Data;
using JWTAuthAPI.Infrastructure.Extensions;
using JWTAuthAPI.Infrastructure.Repositories;
using JWTAuthAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTAuthAPI.Infrastructure.Interceptors;
using JWTAuthAPI.Infrastructure.Configurations;

namespace JWTAuthAPI.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

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

            services.AddJwtAuthentication(configuration);

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, JwtTokenService>();

            services.AddAuthorization(options =>
                options.AddPolicy("CanGetAllUsers", policy => policy.RequireRole(Roles.ADMIN)));

            return services;
        }
    }
}
