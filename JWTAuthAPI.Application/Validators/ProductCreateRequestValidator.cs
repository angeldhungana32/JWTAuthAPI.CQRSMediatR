using FluentValidation;
using JWTAuthAPI.Core.DTOs.Product;

namespace JWTAuthAPI.Application.Validators
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator() 
        {
            RuleFor(v => v.Name)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(100);

            RuleFor(v => v.Price)
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
