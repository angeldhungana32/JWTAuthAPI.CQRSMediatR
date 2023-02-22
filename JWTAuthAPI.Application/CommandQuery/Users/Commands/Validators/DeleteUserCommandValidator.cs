using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
