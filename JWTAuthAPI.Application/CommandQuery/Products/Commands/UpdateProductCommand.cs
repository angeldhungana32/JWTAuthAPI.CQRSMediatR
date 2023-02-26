using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Application.Helpers;
using JWTAuthAPI.Core.Interfaces;
using MediatR;
using JWTAuthAPI.Application.Authorization.Policies;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands
{
    public record UpdateProductCommand : IRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IRepositoryActivator _repositoryActivator;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateProductCommandHandler(IRepositoryActivator repositoryActivator, IResourceAuthorizationService resourceAuthorizationService)
        {
            _repositoryActivator = repositoryActivator;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Guid id = GuidParser.Parse(request.Id);

            var entity = await _repositoryActivator.Repository<Product>().GetByIdAsync(id);

            if(entity == null) 
                throw new NotFoundException(nameof(entity), request.Id);

            var authorized = await _resourceAuthorizationService.AuthorizeAsync(entity,
                ResourcePolicies.UpdateResource.Requirements);

            if (!authorized) throw new ForbiddenAccessException();

            await _repositoryActivator.Repository<Product>().UpdateAsync(entity);
        }

    }
}
