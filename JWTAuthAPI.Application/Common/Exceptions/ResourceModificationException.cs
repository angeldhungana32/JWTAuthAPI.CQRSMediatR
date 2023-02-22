namespace JWTAuthAPI.Application.Common.Exceptions
{
    public class ResourceModificationException : Exception
    {
        public ResourceModificationException() : base() { }
        public ResourceModificationException(string message) : base(message) { }
        public ResourceModificationException(string message, Exception innerException) : 
            base(message, innerException) { }
        public ResourceModificationException(string name, object key) : 
            base($"Unable to Modify: Entity \"{name}\" ({key})") { }
    }
}
