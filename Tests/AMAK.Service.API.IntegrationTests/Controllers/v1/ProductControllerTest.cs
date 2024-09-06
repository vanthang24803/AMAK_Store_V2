using System.Net;
using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Commands.Categories;
using AMAK.Application.Services.Product.Commands.Create;
using AMAK.Application.Services.Product.Commands.Delete;
using AMAK.Application.Services.Product.Commands.Import;
using AMAK.Application.Services.Product.Commands.Option;
using AMAK.Application.Services.Product.Commands.Update;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Product.Queries.Export;
using AMAK.Application.Services.Product.Queries.GetAll;
using AMAK.Application.Services.Product.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {

    public class ProductControllerTest {
        private readonly Guid productId;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;
        private readonly ProductResponse productResponse;
        private readonly Mock<IFormFile> fileMock;
        public ProductControllerTest() {
            // TODO: Arrange
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
            fileMock = new Mock<IFormFile>();
            productId = Guid.NewGuid();
            productResponse = new ProductResponse(
                        Guid.NewGuid(),
                        "Người Là Ai Giữa Tâm Tư Này - Tập 1",
                        "XYZ",
                        "thumbnail.jpg",
                        0,
                        [],
                        [],
                        [],
                        DateTime.UtcNow
            );
        }

        [Fact]
        public async Task Get_All_Products_On_Success_ReturnOK() {
            // TODO: Arrange
            var query = new ProductQuery();
            var expectedResponse = new PaginationResponse<List<ProductResponse>>() {
                Result = [],
                CurrentPage = query.Page,
                TotalPage = 10,
                Items = query.Limit,
                TotalItems = 1000,

            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductQuery>(), default))
                         .ReturnsAsync(expectedResponse);

            // TODO: Act
            var result = await _controller.GetAll(query);

            // TODO: Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }


        [Fact]
        public async Task Get_Detail_Products_On_Success_ReturnOK() {
            var expectedResponse = new Response<ProductDetailResponse>(HttpStatusCode.OK,
               new ProductDetailResponse(
                    productId,
                    "Người Là Ai Giữa Tâm Tư Này - Tập 1",
                    "XYZ",
                    "thumbnail.jpg",
                    1500,
                    "This is a sample product introduction.",
                    "Specifications details",
                    [],
                    [],
                    [],
                    DateTime.UtcNow.AddMonths(-1),
                    DateTime.UtcNow)
               );

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductDetailQuery>(), default))
                         .ReturnsAsync(expectedResponse);

            // TODO:Act
            var result = await _controller.Get(productId);

            // TODO:Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Import_Excel_On_Success_Return201() {

            var expectedResponse = new Response<string>(HttpStatusCode.Created, "Data imported successfully!");

            _mediatorMock.Setup(m => m.Send(It.IsAny<ImportExcelToProductCommand>(), default))
                        .ReturnsAsync(expectedResponse);


            var result = await _controller.ImportExcel(fileMock.Object);

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(expectedResponse, createdResult.Value);
        }

        [Fact]
        public async Task ExportExcel_ReturnsFileWithCorrectContent_OK() {
            // Arrange
            var expectedContent = new byte[] { 0x01, 0x02, 0x03 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ExportProductCommand>(), default))
                        .ReturnsAsync(expectedContent);

            var context = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = await _controller.ExportExcel();

            // Assert
            Assert.NotNull(result);
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileResult.ContentType);
            Assert.Equal(expectedContent, fileResult.FileContents);
            string contentDisposition = context.Response.Headers.ContentDisposition.ToString();
            Assert.Contains("filename=Export-Data-", contentDisposition);
        }


        [Fact]
        public async Task ExportCSV_ReturnsFileWithCorrectContent_OK() {
            // Arrange
            var expectedContent = new byte[] { 0x01, 0x02, 0x03 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ExportCSVProductCommand>(), default))
                        .ReturnsAsync(expectedContent);

            var context = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = await _controller.ExportCSV();

            // Assert
            Assert.NotNull(result);
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("text/csv", fileResult.ContentType);
            Assert.Equal(expectedContent, fileResult.FileContents);
            string contentDisposition = context.Response.Headers.ContentDisposition.ToString();
            Assert.Contains("filename=Export-Data-", contentDisposition);
        }

        [Fact]
        public async Task ExportJson_ReturnsFileWithCorrectContent_OK() {
            // Arrange
            var expectedContent = new byte[] { 0x01, 0x02, 0x03 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ExportJsonProductQuery>(), default))
                        .ReturnsAsync(expectedContent);

            var context = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = await _controller.ExportJson();

            // Assert
            Assert.NotNull(result);
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/json", fileResult.ContentType);
            Assert.Equal(expectedContent, fileResult.FileContents);
            string contentDisposition = context.Response.Headers.ContentDisposition.ToString();
            Assert.Contains("filename=Export-Data-", contentDisposition);
        }

        [Fact]
        public async Task Post_Create_Product_Return_201() {

            var productRequest = new CreateProductRequest {
                Name = "Người Là Ai Giữa Tâm Tư Này - Tập 1",
                Brand = "XYZ",
                Categories = [],
                Introduction = " lorem is pull....",
                Options = [],
                Specifications = "lorem is sum...."
            };


            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                         .ReturnsAsync(new Response<ProductResponse>(HttpStatusCode.Created, productResponse));

            var result = await _controller.Create(productRequest, fileMock.Object);

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);

            var response = Assert.IsType<Response<ProductResponse>>(createdResult.Value);
            var product = response.Result;

            Assert.NotNull(product);
            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.Equal(productRequest.Name, product.Name);
            Assert.Equal(productRequest.Brand, product.Brand);
        }


        [Fact]
        public async Task Put_Update_Product_Information_200() {
            var updateProductRequest = new UpdateProductRequest() {
                Name = "Người Là Ai Giữa Tâm Tư Này - Tập 1",
                Brand = "XYZ",
            };


            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
                        .ReturnsAsync(new Response<ProductResponse>(HttpStatusCode.OK, productResponse));

            var result = await _controller.Update(productId, updateProductRequest, fileMock.Object);

            var createdResult = Assert.IsType<OkObjectResult>(result);

            var response = Assert.IsType<Response<ProductResponse>>(createdResult.Value);
            var product = response.Result;

            Assert.NotNull(product);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(updateProductRequest.Name, product.Name);
            Assert.Equal(updateProductRequest.Brand, product.Brand);
        }


        [Fact]
        public async Task Put_Update_Product_Categories_Return_200() {
            var request = new UpdateProductCategoryRequest();

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCategoryCommand>(), default))
                       .ReturnsAsync(new Response<string>(HttpStatusCode.OK, "Updated categories successfully!"));

            var result = await _controller.UpdateCategories(productId, request);

            var createdResult = Assert.IsType<OkObjectResult>(result);

            var response = Assert.IsType<Response<string>>(createdResult.Value);

            Assert.NotEmpty(response.Result);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal("Updated categories successfully!", response.Result);
        }

        [Fact]
        public async Task Put_Update_Product_Options_Return_200() {
            var request = new OptionsProductRequest();

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductOptionCommand>(), default))
                       .ReturnsAsync(new Response<string>(HttpStatusCode.OK, "Update option successfully!"));

            var result = await _controller.UpdateOptions(productId, request);

            var createdResult = Assert.IsType<OkObjectResult>(result);

            var response = Assert.IsType<Response<string>>(createdResult.Value);

            Assert.NotEmpty(response.Result);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal("Update option successfully!", response.Result);
        }

        [Fact]
        public async Task Delete_Product_Return_200() {
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                       .ReturnsAsync(new Response<string>(HttpStatusCode.OK, "Product deleted successfully!"));

            var result = await _controller.Delete(productId);

            var createdResult = Assert.IsType<OkObjectResult>(result);

            var response = Assert.IsType<Response<string>>(createdResult.Value);

            Assert.NotEmpty(response.Result);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal("Product deleted successfully!", response.Result);

        }

    }
}