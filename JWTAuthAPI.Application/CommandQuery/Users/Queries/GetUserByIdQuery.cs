using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Queries
{
    public record GetUserByIdQuery(string Id) : IRequest<UserResponse>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);

            if (entity == null) throw new NotFoundException(nameof(ApplicationUser), request.Id);

            return entity.ToResponseDTO();
        }
    }

}
