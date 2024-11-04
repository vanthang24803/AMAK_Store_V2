
using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Review;
using AMAK.Application.Services.Review.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]

    public class ReviewsController : BaseController {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService) {
            _reviewService = reviewService;
        }

        [HttpGet]

        public async Task<IActionResult> GetByUser([FromQuery] ReviewQuery query) {
            return Ok(await _reviewService.GetAsync(User, query));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetOne([FromRoute] Guid id) {
            return Ok(await _reviewService.GetOneAsync(id));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]

        public async Task<IActionResult> Remove([FromRoute] Guid id) {
            return Ok(await _reviewService.RemoveAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] CreateReviewRequest request, List<IFormFile> photos) {
            return StatusCode(StatusCodes.Status201Created, await _reviewService.CreateAsync(User, request, photos));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Product/{productId:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid productId, [FromQuery] ReviewQuery query) {
            return Ok(await _reviewService.GetAllAsync(productId, query));
        }
    }
}