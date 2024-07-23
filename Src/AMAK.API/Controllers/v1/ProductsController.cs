using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Commands.Create;
using AMAK.Application.Services.Product.Commands.Delete;
using AMAK.Application.Services.Product.Commands.Export;
using AMAK.Application.Services.Product.Commands.Update;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Product.Queries.GetAll;
using AMAK.Application.Services.Product.Queries.GetDetail;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]

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

        [HttpGet]
        [Route("ExportExcel")]
        [AllowAnonymous]
        public async Task<IActionResult> Export() {
            var result = await _mediator.Send(new ExportProductCommand());

            var now = DateTime.UtcNow.ToString("dd-MM-yyyy");

            Response.Headers.Append("Content-Disposition", $"attachment; filename=Export-Data-{now}.xlsx");

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request, IFormFile thumbnail) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new CreateProductCommand(request, thumbnail)));
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductRequest request, IFormFile? file) {
            return Ok(await _mediator.Send(new UpdateProductCommand(id, file, request)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }

    }
}