using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Application.Helpers;
using JWTAuthAPI.Core.Interfaces;
using MediatR;

namespace JWTAuthAPI.Application.CommandQuery.Products.Queries
{
    public record GetProductByIdQuery(string Id) : IRequest<ProductResponse>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public GetProductByIdQueryHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Guid id = GuidParser.Parse(request.Id);

            var entity = await _repositoryActivator.Repository<Product>().GetByIdAsync(id);

            if (entity == null) 
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            return entity.ToResponseDTO();
        }
    }
}
