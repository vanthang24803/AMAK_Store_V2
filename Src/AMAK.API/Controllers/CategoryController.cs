
using AMAK.Application.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name) {
            return Ok(await _categoryService.SaveAsync(name));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name) {
            return Ok(await _categoryService.GetAllAsync(name));
        }
    }
}