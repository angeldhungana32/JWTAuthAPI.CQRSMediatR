using JWTAuthAPI.Core.Entities;
using System.Security.Claims;

namespace JWTAuthAPI.Core.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<IReadOnlyList<Product>> ListAllProductsByUserIdAsync(string id);
        Task<Product> AddProductAsync(Product entity);
        Task<bool> UpdateProductAsync(Product entity);
        Task<bool> DeleteProductAsync(Product entity);
        Task<bool> AuthorizeProductOwnerAsync(ClaimsPrincipal userContext, Product resource);
    }
}
