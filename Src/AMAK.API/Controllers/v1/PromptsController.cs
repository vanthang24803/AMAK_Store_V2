using AMAK.Application.Common.Constants;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class PromptsController : BaseController {

    }
}