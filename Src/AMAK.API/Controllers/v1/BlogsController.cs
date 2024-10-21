using AMAK.Application.Common.Query;
using AMAK.Application.Services.Blog;
using AMAK.Application.Services.Blog.Dto;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class BlogsController : BaseController {
        private readonly IBlogService _blogService;
        public BlogsController(IBlogService blogService) {
            _blogService = blogService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery query) {
            return Ok(await _blogService.GetAllBlogAsync(query));
        }

        [HttpGet]
        [Route("Author")]
        public async Task<IActionResult> GetAllForAccount([FromQuery] BaseQuery query) {
            return Ok(await _blogService.GetAllBlogForAccountAsync(User, query));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] BlogRequest request, IFormFile thumbnail) {
            return StatusCode(StatusCodes.Status201Created, await _blogService.CreateAsync(User, request, thumbnail));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] BlogRequest request, IFormFile? thumbnail) {
            return Ok(await _blogService.UpdateAsync(User, id, request, thumbnail));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindOne(Guid id) {
            return Ok(await _blogService.FindOneByIdAsync(id));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id) {
            return Ok(await _blogService.DeleteAsync(User, id));
        }
    }
}