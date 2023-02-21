using JWTAuthAPI.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.API.Controllers.v1
{
    [Route(RouteConstants.DefaultControllerRoutev1)]
    [ApiController]
    [Authorize]
    public class V1BaseController : ControllerBase
    {
    }
}
