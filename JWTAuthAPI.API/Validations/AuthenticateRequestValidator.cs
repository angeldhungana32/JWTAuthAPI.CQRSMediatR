using FluentValidation;
using JWTAuthAPI.Core.DTOs.Authentication;

namespace JWTAuthAPI.API.Validations
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(300);

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
