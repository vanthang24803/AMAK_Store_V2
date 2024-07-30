
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

        // [HttpGet]
        // [Route("Products/{productId:guid}/[controller]")]

        // public Task<IActionResult> GetAll(Guid productId) {

        // }

        [HttpPost]

        public async Task<IActionResult> Save([FromForm] CreateReviewRequest request, List<IFormFile> photos) {
            return StatusCode(StatusCodes.Status201Created, await _reviewService.CreateAsync(User, request, photos));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Products/{productId:guid}/[controller]")]
        public async Task<IActionResult> Get([FromRoute] Guid productId) {
            return Ok(await _reviewService.GetAllAsync(productId));
        }
    }
}