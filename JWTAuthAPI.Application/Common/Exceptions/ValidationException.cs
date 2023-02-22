using FluentValidation.Results;
using System.Text.Json;

namespace JWTAuthAPI.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }
        public ValidationException(IEnumerable<ValidationFailure> failures) : base(ValidationExceptionToString(failures)){ }


        private static string ValidationExceptionToString(IEnumerable<ValidationFailure> failures)
        {
            IDictionary<string, string[]> Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            return JsonSerializer.Serialize(Errors);
        }

    }
}
