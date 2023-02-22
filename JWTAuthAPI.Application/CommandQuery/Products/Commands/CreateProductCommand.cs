using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Interfaces;
using MediatR;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands
{
    public record CreateProductCommand : IRequest<ProductResponse>
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public CreateProductCommandHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            entity = await _repositoryActivator.Repository<Product>().AddAsync(entity);

            return entity.ToResponseDTO();
        }

    }
}
