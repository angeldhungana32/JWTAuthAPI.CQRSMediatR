using JWTAuthAPI.Application.Authorization.Attributes;
using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands
{
    [AuthorizeCustom(Policy = "UserIsOwner")]
    public record UpdateUserCommand : IRequest
    {
        public string? Id { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);
            if(entity == null) 
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            var result = await _userManager.UpdateAsync(entity.UpdateEntity(request));

            if(!result.Succeeded) 
                throw new ResourceModificationException(nameof(ApplicationUser), request.Id);
        }
    }
}
