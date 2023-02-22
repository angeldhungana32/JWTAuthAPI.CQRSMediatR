namespace JWTAuthAPI.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}