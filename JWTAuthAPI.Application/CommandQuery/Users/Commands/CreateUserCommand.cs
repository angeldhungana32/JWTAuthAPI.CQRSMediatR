using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands
{
    public record CreateUserCommand : IRequest<UserResponse>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();
            var result = await _userManager.CreateAsync(entity, request.Password); 
            
            if(!result.Succeeded) 
                throw new ResourceCreationException(nameof(ApplicationUser));

            result = await _userManager.AddToRoleAsync(entity, Roles.USER);

            if (!result.Succeeded) 
                throw new ResourceCreationException(nameof(ApplicationUser));

            return entity.ToResponseDTO();
        }
    }
}
