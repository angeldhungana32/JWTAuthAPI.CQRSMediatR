using JWTAuthAPI.Application.Authorization.Attributes;
using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users.Commands
{
    [AuthorizeCustom(Policy = "UserIsOwner")]
    public record DeleteUserCommand(string Id) : IRequest;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);

            if (entity == null) 
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            await _userManager.DeleteAsync(entity);
        }
    }
}
