using AMAK.Application.Validations;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiController]
    [ApiVersion(1)]
    [ValidateModelState]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase {

    }
}