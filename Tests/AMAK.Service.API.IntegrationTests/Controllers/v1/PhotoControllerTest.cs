using System.Net;
using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Photo;
using AMAK.Application.Services.Photo.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class PhotoControllerTest {
        private readonly Guid _photoId = Guid.NewGuid();
        private readonly Guid _productId = Guid.NewGuid();
        private readonly Mock<IPhotoService> _mockPhotoService;
        private readonly List<IFormFile> _mockFiles;
        private readonly List<PhotoResponse> _photoResponses = [];
        private readonly PhotosController _photosController;

        public PhotoControllerTest() {
            _mockPhotoService = new Mock<IPhotoService>();

            var mockFile = new Mock<IFormFile>();
            _mockFiles = [mockFile.Object];

            _photosController = new PhotosController(_mockPhotoService.Object);
        }

        [Fact]
        public async Task Post_Create_Photos_Return_201() {
            // Arrange
            var expectedResponse = new Response<List<PhotoResponse>>(HttpStatusCode.Created, _photoResponses);

            _mockPhotoService
                .Setup(service => service.CreateAsync(_productId, _mockFiles))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _photosController.Create(_productId, _mockFiles);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<List<PhotoResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }

        [Fact]

        public async Task Get_All_Photos_Return_200() {
            var expectedResponse = new Response<List<PhotoResponse>>(HttpStatusCode.OK, _photoResponses);

            _mockPhotoService
              .Setup(service => service.GetAllAsync(_productId))
              .ReturnsAsync(expectedResponse);

            var result = await _photosController.GetAll(_productId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<List<PhotoResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }

        [Fact]

        public async Task Delete_Photo_Return_200() {
            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Delete photo successfully!");

            _mockPhotoService
           .Setup(service => service.DeleteAsync(_productId, _photoId))
           .ReturnsAsync(expectedResponse);

            var result = await _photosController.Delete(_productId, _photoId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }
    }
}
