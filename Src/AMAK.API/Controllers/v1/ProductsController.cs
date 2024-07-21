using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Product;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{StaticRole.ADMIN}, {StaticRole.MANAGER}")]

    public class ProductsController : BaseController {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll() {
            return Ok("Hello World");
        }


    }
}