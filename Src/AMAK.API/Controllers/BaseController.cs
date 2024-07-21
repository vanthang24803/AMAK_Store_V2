using AMAK.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers {
    [ApiController]
    [ValidateModelState]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase {

    }
}