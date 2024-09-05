using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Categories.Commands.Create;
using AMAK.Application.Services.Categories.Commands.Delete;
using AMAK.Application.Services.Categories.Commands.Update;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Categories.Queries.GetAll;
using AMAK.Application.Services.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class CategoryControllerTest {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoriesController _controller;
        private readonly Guid _categoryId;

        public CategoryControllerTest() {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CategoriesController(_mediatorMock.Object);
            _categoryId = Guid.NewGuid();
        }

        [Fact]
        public async Task Get_FindAll_Categories_Return_200() {
            var expectedResponse = new Response<List<CategoryResponse>>(HttpStatusCode.OK, []);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCategoryQuery>(), default))
                    .ReturnsAsync(expectedResponse);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Get_Detail_Category_Return_200() {
            var expectedResponse = new Response<CategoryResponse>(HttpStatusCode.OK, new CategoryResponse(_categoryId, "Sách mới", DateTime.UtcNow));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryDetailQuery>(), default))
                        .ReturnsAsync(expectedResponse);

            var result = await _controller.GetDetail(_categoryId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Post_Create_Category_Return_201() {
            var request = new CategoryRequest() {
                Name = "Sách mới"
            };

            var expectedResponse = new Response<CategoryResponse>(HttpStatusCode.Created, new CategoryResponse(_categoryId, "Sách mới", DateTime.UtcNow));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default))
                        .ReturnsAsync(expectedResponse);

            var result = await _controller.Save(request);

            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Put_Update_Category_Return_200() {
            var request = new CategoryRequest() {
                Name = "Sách mới"
            };

            var expectedResponse = new Response<CategoryResponse>(HttpStatusCode.OK, new CategoryResponse(_categoryId, "Sách mới", DateTime.UtcNow));

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), default))
                        .ReturnsAsync(expectedResponse);

            var result = await _controller.Update(_categoryId, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]

        public async Task Delete_Category_Return_200() {
            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Category deleted successfully!");

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                        .ReturnsAsync(expectedResponse);
            var result = await _controller.Delete(_categoryId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

    }
}