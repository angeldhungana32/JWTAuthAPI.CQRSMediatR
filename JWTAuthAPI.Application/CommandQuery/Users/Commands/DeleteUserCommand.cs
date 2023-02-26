using JWTAuthAPI.Application.Authorization.Policies;
using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands
{
    public record DeleteUserCommand(string Id) : IRequest<bool>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResourceAuthorizationService _authorizationService;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager,
            IResourceAuthorizationService authorizationService) 
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);

            if (entity == null) throw new NotFoundException(nameof(ApplicationUser), request.Id);

            var authorized = await _authorizationService.AuthorizeAsync(entity,
                ResourcePolicies.DeleteResource.Requirements);

            if (!authorized) throw new ForbiddenAccessException();

            var result = await _userManager.DeleteAsync(entity);

            return result.Succeeded;
        }
    }
}
