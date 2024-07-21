using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Category;
using AMAK.Application.Services.Category.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [Authorize(Roles = $"{StaticRole.ADMIN}, {StaticRole.MANAGER}")]
    public class CategoriesController : BaseController {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id) {
            return Ok(await _categoryService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CategoryRequest request) {
            return Ok(await _categoryService.CreateAsync(request));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CategoryRequest request) {
            return Ok(await _categoryService.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _categoryService.DeleteAsync(id));
        }
    }
}