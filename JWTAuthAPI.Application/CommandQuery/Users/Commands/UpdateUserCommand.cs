using JWTAuthAPI.Application.Authorization.Policies;
using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string? Id { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResourceAuthorizationService _authorizationService;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager,
            IResourceAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);

            if(entity == null) 
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            var authorized = await _authorizationService.AuthorizeAsync(entity,
               ResourcePolicies.UpdateResource.Requirements);

            if (!authorized) throw new ForbiddenAccessException();

            var result = await _userManager.UpdateAsync(entity.UpdateEntity(request));

            if(!result.Succeeded) 
                throw new ResourceModificationException(nameof(ApplicationUser), request.Id);

            return result.Succeeded;
        }
    }
}
