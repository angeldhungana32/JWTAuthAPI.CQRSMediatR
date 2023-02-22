﻿using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using MediatR;

namespace JWTAuthAPI.Application.CommandQuery.Users.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserResponse>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public GetAllUsersQueryHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<ApplicationUser> applicationUsers = await _repositoryActivator.Repository<ApplicationUser>().ListAllAsync();
            return applicationUsers.ToResponseDTO(); 
        }
    }

}
