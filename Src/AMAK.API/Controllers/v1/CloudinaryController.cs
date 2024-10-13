using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Providers.Cloudinary;
using AMAK.Application.Providers.Cloudinary.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}")]
    public class CloudinaryController : BaseController {

        private readonly ICloudinaryService _cloudinaryService;

        public CloudinaryController(ICloudinaryService cloudinaryService) {
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromQuery] BaseQuery query) {
            return Ok(await _cloudinaryService.GetAllImages(query));
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteManyImages([FromBody] RemoveCloudinaryRequest request) {
            return Ok(await _cloudinaryService.RemoveImage(request));
        }

        [HttpPost]
        public async Task<IActionResult> SaveManyImages(List<IFormFile> files) {
            return Ok(await _cloudinaryService.UploadCloudImages(files));
        }

    }
}