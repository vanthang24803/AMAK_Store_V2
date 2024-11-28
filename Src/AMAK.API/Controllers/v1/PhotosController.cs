using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Photo;
using AMAK.Application.Validations;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]
    [Route("api/v{version:apiVersion}/Products")]

    public class PhotosController : BaseController {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService) {
            _photoService = photoService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{productId:guid}/[controller]")]
        public async Task<IActionResult> GetAll([FromRoute] Guid productId) {
            return Ok(await _photoService.GetAllAsync(productId));
        }

        [HttpPost]
        [FileValidate]
        [Route("{productId:guid}/[controller]")]
        public async Task<IActionResult> Create([FromRoute] Guid productId, List<IFormFile> photos) {
            return StatusCode(StatusCodes.Status201Created, await _photoService.CreateAsync(productId, photos));
        }

        [HttpDelete]
        [Route("{productId:guid}/[controller]/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId, [FromRoute] Guid id) {
            return Ok(await _photoService.DeleteAsync(productId, id));
        }
    }
}