using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(v => v.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
