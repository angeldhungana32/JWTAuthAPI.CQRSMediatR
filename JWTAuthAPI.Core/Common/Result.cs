namespace JWTAuthAPI.Core.Common
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public T? ResponseObject {get; set;}

        private Result(bool succeeded, IEnumerable<string>? errors, T? responseObject)
        {
            Succeeded = succeeded;
            Errors = errors?.ToArray() ?? Array.Empty<string>();
            ResponseObject = responseObject;
        }

        public static Result<T> Success()
        {
            return new Result<T>(true, null, default);
        }

        public static Result<T> Success(T? responseObject)
        {
            return new Result<T>(true, null, responseObject);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors, default);
        }

    }
}
