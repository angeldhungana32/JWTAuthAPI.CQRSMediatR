using JWTAuthAPI.Core.DTOs.Authentication;
using JWTAuthAPI.Core.Entities.Identity;
using System.Security.Claims;

namespace JWTAuthAPI.Core.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password);
        Task<bool> UpdateUserAsync(ApplicationUser entity);
        Task<bool> DeleteUserAsync(ApplicationUser entity);
        Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request);
        Task<bool> AuthorizeOwnerAsync(ClaimsPrincipal userContext, ApplicationUser resource);
        Task<IReadOnlyList<ApplicationUser>> GetAllUsers();
    }
}
