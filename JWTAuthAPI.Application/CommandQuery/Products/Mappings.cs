using JWTAuthAPI.Application.CommandQuery.Products.Commands;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Helpers;

namespace JWTAuthAPI.Application.CommandQuery.Products
{
    public static class Mappings
    {
        public static ProductResponse ToResponseDTO(this Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            return new ProductResponse()
            {
                Id = product.Id.ToString(),
                Description = product.Description,
                Price = product.Price,
                Name = product.Name,
                Quantity = product.Quantity,
                UserId = product.UserId.ToString()
            };
        }

        public static List<ProductResponse> ToResponseDTO(this IReadOnlyList<Product> products)
        {
            List<ProductResponse> productsResponse = new();

            if (products != null)
            {
                productsResponse.AddRange(products.Select(product => product.ToResponseDTO()));
            }

            return productsResponse;
        }

        public static Product ToEntity(this CreateProductCommand request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new Product()
            {
                Description = request.Description,
                Name = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                UserId = GuidParser.Parse(request.UserId),
            };
        }

        public static Product UpdateEntity(this Product product, UpdateProductCommand request)
        {
            if (request != null)
            {
                product.Price = request.Price;
                product.Description = request.Description;
                product.Quantity = request.Quantity;
                product.Name = request.Name;
            }

            return product;

        }

    }
}
