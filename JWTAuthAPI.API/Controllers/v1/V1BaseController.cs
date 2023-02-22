using JWTAuthAPI.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.API.Controllers.v1
{
    [Route(RouteConstants.DefaultControllerRoutev1)]
    [ApiController]
    [Authorize]
    public abstract class V1BaseController : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
