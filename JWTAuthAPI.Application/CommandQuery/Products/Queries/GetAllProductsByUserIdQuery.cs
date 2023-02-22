using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Application.Helpers;
using JWTAuthAPI.Core.Interfaces;
using MediatR;
using JWTAuthAPI.Core.Specifications;

namespace JWTAuthAPI.Application.CommandQuery.Products.Queries
{
    public record GetAllProductsByUserIdQuery(string Id) : IRequest<List<ProductResponse>>;

    public class GetAllProductsByUserIdQueryHandler : IRequestHandler<GetAllProductsByUserIdQuery, List<ProductResponse>>
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public GetAllProductsByUserIdQueryHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            Guid id = GuidParser.Parse(request.Id);

            IReadOnlyList<Product> products = await _repositoryActivator.Repository<Product>().ListAllAsync(new ProductsByUserId(id));

            return products.ToResponseDTO();
        }
    }

    public class ProductsByUserId : BaseSpecification<Product>
    {
        public ProductsByUserId(Guid userId) : base(x => x.UserId == userId) { }
    }
}
