using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Commands.Categories;
using AMAK.Application.Services.Product.Commands.Create;
using AMAK.Application.Services.Product.Commands.Delete;
using AMAK.Application.Services.Product.Queries.Export;
using AMAK.Application.Services.Product.Commands.Option;
using AMAK.Application.Services.Product.Commands.Update;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Product.Queries.GetAll;
using AMAK.Application.Services.Product.Queries.GetDetail;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AMAK.Application.Services.Product.Commands.Import;
using AMAK.Application.Services.Product.Commands.Thumbnail;
using AMAK.Application.Validations;


namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]

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
        [Route("{id:guid}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new GetProductDetailQuery(id)));
        }

        [HttpPost]
        [FileValidate]
        [Route("ImportExcel")]
        [AllowAnonymous]

        public async Task<IActionResult> ImportExcel(IFormFile file) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new ImportExcelToProductCommand(file)));
        }

        [HttpGet]
        [Route("ExportExcel")]
        [AllowAnonymous]
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
            var fileName = $"Export-Data-{now}.csv";

            Response.Headers.Append("Content-Disposition", $"attachment; filename={fileName}");

            return File(result, "text/csv");
        }


        [HttpGet]
        [Route("ExportJson")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportJson() {
            var result = await _mediator.Send(new ExportJsonProductQuery());

            var now = DateTime.UtcNow.ToString("dd-MM-yyyy");
            var fileName = $"Export-Data-{now}.json";

            Response.Headers.Append("Content-Disposition", $"attachment; filename={fileName}");

            return File(result, "application/json", fileName);
        }

        [HttpPost]
        [FileValidate]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request, IFormFile thumbnail) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new CreateProductCommand(request, thumbnail)));
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request) {
            return Ok(await _mediator.Send(new UpdateProductCommand(id, request)));
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

        [HttpPost]
        [FileValidate]
        [Route("{id:guid}/Thumbnail")]
        public async Task<IActionResult> UpdateThumbnail([FromRoute] Guid id, IFormFile thumbnail) {
            return Ok(await _mediator.Send(new UpdateThumbnailProductCommand(id, thumbnail)));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = $"{Role.Admin}")]
        public async Task<IActionResult> Delete(Guid id) {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }

    }
}