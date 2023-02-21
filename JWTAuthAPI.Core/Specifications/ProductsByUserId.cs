using JWTAuthAPI.Core.Entities;

namespace JWTAuthAPI.Core.Specifications
{
    public class ProductsByUserId : BaseSpecification<Product>
    {
        public ProductsByUserId(Guid userId) : base(x => x.UserId == userId) { }
    }
}
