using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Trash;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class TrashController : BaseController {
        private readonly ITrashService _trashService;

        public TrashController(ITrashService trashService) {
            _trashService = trashService;
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> GetTrashProduct() {
            return Ok(await _trashService.GetProductTrashAsync());
        }

        [HttpGet]
        [Route("Option")]
        public async Task<IActionResult> GetTrashOption() {
            return Ok(await _trashService.GetOptionTrashAsync());
        }

        [HttpGet]
        [Route("Media")]
        public async Task<IActionResult> GetTrashMedia() {
            return Ok(await _trashService.GetPhotoResponse());
        }
    }
}