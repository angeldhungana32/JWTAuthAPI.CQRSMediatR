using FluentValidation;

namespace JWTAuthAPI.Application.CommandQuery.Users.Queries.Validators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
