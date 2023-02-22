using JWTAuthAPI.Application.CommandQuery.Users.Commands;
using JWTAuthAPI.Core.Entities.Identity;

namespace JWTAuthAPI.Application.CommandQuery.Users
{
    public static class Mappings
    {
        public static UserResponse ToResponseDTO(this ApplicationUser user)
        {
            if (user == null) 
                throw new ArgumentNullException(nameof(user));

            return new UserResponse()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static List<UserResponse> ToResponseDTO(this IReadOnlyCollection<ApplicationUser> users)
        {
            List<UserResponse> usersResponse = new();

            if (users != null)
                usersResponse.AddRange(users.Select(user => user.ToResponseDTO()));

            return usersResponse;
        }

        public static ApplicationUser ToEntity(this CreateUserCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));

            return new ApplicationUser()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserName = command.Email
            };
        }

        public static ApplicationUser UpdateEntity(this ApplicationUser user, UpdateUserCommand request)
        {
            if (request != null)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
            }

            return user;
        }
    }
}
