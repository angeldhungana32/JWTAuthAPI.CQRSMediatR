using JWTAuthAPI.Application.CommandQuery.Products;
using JWTAuthAPI.Application.CommandQuery.Products.Commands;
using JWTAuthAPI.Application.CommandQuery.Products.Queries;
using JWTAuthAPI.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.API.Controllers.v1
{
    public class ProductsController : V1BaseController
    {
        // POST api/v1/Products
        [HttpPost(RouteConstants.AddProduct)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductCommand command)
        {
            var product = await Mediator.Send(command);

            return Created(string.Format("api/v1/Products/{0}", product.Id), product);
        }

        // GET api/v1/Products/id
        [HttpGet(RouteConstants.GetProduct)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery(id)));
        }

        // PUT api/v1/Products/id
        [HttpPut(RouteConstants.UpdateProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductAsync(string id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/v1/Products/id
        [HttpDelete(RouteConstants.DeleteProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            await Mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }

        [HttpGet(RouteConstants.GetAllProductsByUserId)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByUserIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetAllProductsByUserIdQuery(id)));
        }
    }
}
