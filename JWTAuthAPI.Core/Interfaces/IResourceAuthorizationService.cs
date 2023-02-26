using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Core.Interfaces
{
    public interface IResourceAuthorizationService
    {
        Task<bool> AuthorizeAsync(ApplicationUser resource, IReadOnlyList<IAuthorizationRequirement> requirements);
        Task<bool> AuthorizeAsync(Product resource, IReadOnlyList<IAuthorizationRequirement> requirements);
    }
}
