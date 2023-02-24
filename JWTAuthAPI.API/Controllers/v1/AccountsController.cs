using JWTAuthAPI.Application.Authorization.Attributes;
using JWTAuthAPI.Application.CommandQuery.Authentication;
using JWTAuthAPI.Application.CommandQuery.Authentication.Commands;
using JWTAuthAPI.Application.CommandQuery.Users;
using JWTAuthAPI.Application.CommandQuery.Users.Commands;
using JWTAuthAPI.Application.CommandQuery.Users.Queries;
using JWTAuthAPI.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.API.Controllers.v1
{
    public class AccountsController : V1BaseController
    {
        // POST api/v1/Accounts/Login
        [AllowAnonymous]
        [HttpPost(RouteConstants.Login)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/v1/Accounts/Register
        [AllowAnonymous]
        [HttpPost(RouteConstants.Register)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Created(string.Format("api/v1/Accounts/Users/{0}", response.Id), response);
        }

        // GET api/v1/Accounts/Users/id
        [HttpGet(RouteConstants.GetUser)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery(id)));
        }

        // GET api/v1/Accounts/Users/
        [HttpGet(RouteConstants.GetAllUsers)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        // PUT api/v1/Accounts/Users/id
        [HttpPut(RouteConstants.UpdateUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id) { return BadRequest(); }

            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/v1/Accounts/Users/id
        [HttpDelete(RouteConstants.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await Mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}
