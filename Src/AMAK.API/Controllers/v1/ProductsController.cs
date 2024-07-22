using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Commands.Create;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Product.Queries.GetAll;
using AMAK.Application.Services.Product.Queries.GetDetail;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{StaticRole.ADMIN}, {StaticRole.MANAGER}")]

    public class ProductsController : BaseController {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ProductQuery query) {
            return Ok(await _mediator.Send(new GetAllProductQuery(query)));
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new GetProductDetailQuery(id)));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request, IFormFile thumbnail) {
            return Ok(await _mediator.Send(new CreateProductCommand(request, thumbnail)));
        }

    }
}