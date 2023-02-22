namespace JWTAuthAPI.Application.Common.Exceptions
{
    public class ResourceCreationException : Exception
    {
        public ResourceCreationException() : base() { }
        public ResourceCreationException(string message, Exception innerException) : 
            base(message, innerException) { }

        public ResourceCreationException(string name) : base($"Unable to create Entity \"{name}\"") { }

    }
}
