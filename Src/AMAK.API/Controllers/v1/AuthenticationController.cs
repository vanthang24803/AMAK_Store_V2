using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AuthenticationController : ControllerBase {
        [HttpGet]
        public IActionResult Get() {
            return Ok("Hello World");
        }
    }
}