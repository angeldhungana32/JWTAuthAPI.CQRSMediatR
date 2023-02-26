using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Infrastructure.Services
{
    public class ResourceAuthorizationService : IResourceAuthorizationService
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUserService _currentUserService;

        public ResourceAuthorizationService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _currentUserService = currentUserService;
        }
        public async Task<bool> AuthorizeAsync(Product resource, IReadOnlyList<IAuthorizationRequirement> requirements)
        {
            var principal = await CreatePrincipal(_currentUserService.UserId ?? null);

            if (principal == null) return false;

            var result = await _authorizationService.AuthorizeAsync(principal, resource, requirements);
            return result == null || result.Succeeded;
        }

        public async Task<bool> AuthorizeAsync(ApplicationUser resource, IReadOnlyList<IAuthorizationRequirement> requirements)
        {
            var principal = await CreatePrincipal(_currentUserService.UserId ?? null);

            if (principal == null) return false;

            var result = await _authorizationService.AuthorizeAsync(principal, resource, requirements);

            return result == null || result.Succeeded;
        }

        private async Task<ClaimsPrincipal?> CreatePrincipal(string? userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            var user = await _userManager.FindByIdAsync(userId);
            return user == null ? null : await _userClaimsPrincipalFactory.CreateAsync(user);
        }
    }
}
