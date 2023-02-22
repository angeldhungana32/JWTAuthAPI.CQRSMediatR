using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Authentication.Commands.Validators
{

    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(v => v.Password)
               .NotNull().WithMessage("Password is required.")
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(8).WithMessage("Your password length must be at least 8.")
               .MaximumLength(20).WithMessage("Your password length must not exceed 20.")
               .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
               .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
               .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
               .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(v => v.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress()
                .MaximumLength(300);
        }
    }
}
