using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Commands.Categories;
using AMAK.Application.Services.Product.Commands.Create;
using AMAK.Application.Services.Product.Commands.Delete;
using AMAK.Application.Services.Product.Commands.Export;
using AMAK.Application.Services.Product.Commands.Option;
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
        public async Task<IActionResult> ExportExcel() {
            var result = await _mediator.Send(new ExportProductCommand());

            var now = DateTime.UtcNow.ToString("dd-MM-yyyy");

            Response.Headers.Append("Content-Disposition", $"attachment; filename=Export-Data-{now}.xlsx");

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        [HttpGet]
        [Route("ExportCSV")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportCSV() {
            var result = await _mediator.Send(new ExportCSVProductCommand());

            var now = DateTime.UtcNow.ToString("dd-MM-yyyy");
            var fileName = $"Products_{now}.csv";

            return File(result, "text/csv", fileName);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request, IFormFile thumbnail) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new CreateProductCommand(request, thumbnail)));
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductRequest request, IFormFile? file) {
            return Ok(await _mediator.Send(new UpdateProductCommand(id, file, request)));
        }

        [HttpPut]
        [Route("{id:guid}/Categories")]
        public async Task<IActionResult> UpdateCategories([FromRoute] Guid id, [FromBody] UpdateProductCategoryRequest request) {
            return Ok(await _mediator.Send(new UpdateProductCategoryCommand(id, request)));
        }

        [HttpPut]
        [Route("{id:guid}/Options")]
        public async Task<IActionResult> UpdateOptions([FromRoute] Guid id, [FromBody] OptionsProductRequest request) {
            return Ok(await _mediator.Send(new UpdateProductOptionCommand(id, request)));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = $"{Role.ADMIN}")]
        public async Task<IActionResult> Delete(Guid id) {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }

    }
}