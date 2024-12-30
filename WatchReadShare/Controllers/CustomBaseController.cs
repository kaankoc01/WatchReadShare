using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WatchReadShare.Application;

namespace WatchReadShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (result.Status == HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result);
            }

            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
            }
            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
    }
}
