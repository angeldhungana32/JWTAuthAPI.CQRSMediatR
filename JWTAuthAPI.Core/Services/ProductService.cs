using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Helpers;
using JWTAuthAPI.Core.Interfaces;
using JWTAuthAPI.Core.Specifications;
using System.Security.Claims;

namespace JWTAuthAPI.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public ProductService(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _repositoryActivator.Repository<Product>()
                .AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            return await _repositoryActivator.Repository<Product>()
                .DeleteAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            Guid guid = GuidParser.Parse(id);
            return await _repositoryActivator.Repository<Product>()
                .GetByIdAsync(guid);
        }

        public async Task<IReadOnlyList<Product>> ListAllProductsByUserIdAsync(string id)
        {
            Guid guid = GuidParser.Parse(id);
            return await _repositoryActivator.Repository<Product>()
                .ListAllAsync(new ProductsByUserId(guid));
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _repositoryActivator.Repository<Product>()
                .UpdateAsync(product);
        }
    }
}
