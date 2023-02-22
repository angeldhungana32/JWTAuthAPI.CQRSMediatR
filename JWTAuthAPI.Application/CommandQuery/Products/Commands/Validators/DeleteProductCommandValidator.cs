using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty();

        }
    }
}
