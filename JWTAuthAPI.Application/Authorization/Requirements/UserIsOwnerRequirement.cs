using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application.Authorization.Requirements
{
    public class UserIsOwnerRequirement : IAuthorizationRequirement { }
}
