using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(100);

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Quantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(v => v.UserId)
                .NotNull()
                .NotEmpty();

        }
    }
}
