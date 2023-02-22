using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Products.Queries.Validators
{
    public class GetAllProductsByUserIdQueryValidator : AbstractValidator<GetAllProductsByUserIdQuery>
    {
        public GetAllProductsByUserIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
