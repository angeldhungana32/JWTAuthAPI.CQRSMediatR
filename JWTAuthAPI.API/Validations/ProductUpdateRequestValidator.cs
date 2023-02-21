using FluentValidation;
using JWTAuthAPI.Core.DTOs.Product;

namespace JWTAuthAPI.API.Validations
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator() 
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
        }
    }
}
