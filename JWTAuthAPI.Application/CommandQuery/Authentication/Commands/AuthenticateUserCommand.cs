using FluentValidation.Results;
using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Authentication.Commands
{
    public record AuthenticateUserCommand : IRequest<AuthenticateResponse>
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticateUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByEmailAsync(request.Email);

            if(entity == null) ThrowValidationError();

            var isValidUser = await _userManager.CheckPasswordAsync(entity, request.Password);

            if(!isValidUser) ThrowValidationError();

            var roles = await _userManager.GetRolesAsync(entity);
            string token = _tokenService.GenerateAuthenticationToken(entity, (List<string>)roles);

            return entity.ToResponseDTO(token);
        }

        private static void ThrowValidationError()
        {
            List<ValidationFailure> validationFailures = new ()
            {
                new ValidationFailure()
                {
                    PropertyName = "Email/Password",
                    ErrorMessage = "Invalid Email Or Password"
                }
            };

            throw new ValidationException(validationFailures);
        }
    }
}
