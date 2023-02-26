using System.Security.Claims;

namespace JWTAuthAPI.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}