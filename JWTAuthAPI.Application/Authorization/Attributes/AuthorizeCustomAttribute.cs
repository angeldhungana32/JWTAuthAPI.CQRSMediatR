namespace JWTAuthAPI.Application.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeCustomAttribute : Attribute
    {
        public AuthorizeCustomAttribute() { }

        public string Roles { get; set; } = string.Empty;

        public string Policy { get; set; } = string.Empty;
    }
}
