using JWTAuthAPI.Application.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application.Authorization.Policies
{
    static class ResourceOperations
    {
        public static UserIsOwnerRequirement ReadRequirement = new();
        public static UserIsOwnerRequirement UpdateRequirement = new();
        public static UserIsOwnerRequirement DeleteRequirement = new();
    }

    static class ResourcePolicies
    {
        public static AuthorizationPolicy CreateResource = new AuthorizationPolicyBuilder()
               .AddRequirements(ResourceOperations.ReadRequirement)
               .Build();
        public static AuthorizationPolicy DeleteResource = new AuthorizationPolicyBuilder()
                .AddRequirements(ResourceOperations.DeleteRequirement)
                .Build();
        public static AuthorizationPolicy UpdateResource = new AuthorizationPolicyBuilder()
                .AddRequirements(ResourceOperations.UpdateRequirement)
                .Build();
    }
}
