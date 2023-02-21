using JWTAuthAPI.Core.Entities.Identity;

namespace JWTAuthAPI.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateAuthenticationToken(ApplicationUser user, List<string> roles);
    }
}
