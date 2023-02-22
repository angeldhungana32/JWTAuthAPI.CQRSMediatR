using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Products.Queries.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
