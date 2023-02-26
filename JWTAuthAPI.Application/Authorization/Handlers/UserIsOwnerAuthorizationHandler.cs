using JWTAuthAPI.Application.Authorization.Policies;
using JWTAuthAPI.Application.Authorization.Requirements;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Infrastructure.Authorization
{
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<UserIsOwnerRequirement, ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public UserIsOwnerAuthorizationHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsOwnerRequirement requirement, ApplicationUser resource)
        {
            if (_currentUserService.UserId == null || resource == null) { return Task.CompletedTask; }


            if(requirement == ResourceOperations.DeleteRequirement || 
                requirement == ResourceOperations.UpdateRequirement)
            {
                if (resource.Id.ToString() == _currentUserService.UserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public class UserIsProductOwnerAuthorizationHandler : AuthorizationHandler<UserIsOwnerRequirement, Product>
    {
        private readonly ICurrentUserService _currentUserService;

        public UserIsProductOwnerAuthorizationHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsOwnerRequirement requirement, Product resource)
        {
            if (_currentUserService.UserId == null || resource == null) { return Task.CompletedTask; }

            if (resource.UserId.ToString() == _currentUserService.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
