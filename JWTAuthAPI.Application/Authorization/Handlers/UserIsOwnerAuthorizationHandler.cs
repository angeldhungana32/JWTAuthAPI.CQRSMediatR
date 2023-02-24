using JWTAuthAPI.Core.AuthorizationRequirement;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application.Authorization.Handlers
{
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<UserIsOwnerRequirement, ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public UserIsOwnerAuthorizationHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserIsOwnerRequirement requirement,
            ApplicationUser resource)
        {
            if (_currentUserService.UserId == null || resource == null) { return Task.CompletedTask; }

            if (resource.Id.ToString() == _currentUserService.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
