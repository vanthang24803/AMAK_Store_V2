using Moq;
using System.Net;
using AMAK.Application.Services.Options;
using AMAK.Application.Services.Options.Dtos;
using Microsoft.AspNetCore.Mvc;
using AMAK.Application.Common.Helpers;
using AMAK.API.Controllers.v1;


namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class OptionControllerTest {
        private readonly Guid _optionId = Guid.NewGuid();
        private readonly Guid _productId = Guid.NewGuid();
        private readonly Mock<IOptionsService> _mockOptionsService;
        private readonly OptionsController _controller;
        private readonly OptionRequest _request;
        private readonly Response<OptionResponse> _response;

        public OptionControllerTest() {
            _mockOptionsService = new Mock<IOptionsService>();
            _controller = new OptionsController(_mockOptionsService.Object);
            _request = new OptionRequest() {
                Name = "Thường",
                Price = 10000,
                Quantity = 1000,
                Sale = 10,
                IsActive = true
            };
            _response = new Response<OptionResponse>(HttpStatusCode.OK, new OptionResponse(
                _optionId,
                "Thường",
                10,
                10000,
                1000,
                true,
                DateTime.UtcNow
            ));
        }

        [Fact]
        public async Task Get_FindAll_Options_Return_200() {
            // Arrange
            var options = new List<OptionResponse>();

            var expectedResponse = new Response<List<OptionResponse>>(HttpStatusCode.OK, options);

            _mockOptionsService
                .Setup(service => service.GetAllAsync(_productId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAll(_productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<List<OptionResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]
        public async Task Get_Detail_Option_Return_200() {
            _mockOptionsService
                .Setup(service => service.GetAsync(_productId, _optionId))
                .ReturnsAsync(_response);

            var result = await _controller.GetDetail(_productId, _optionId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<OptionResponse>>(okResult.Value);
            Assert.Equal(_response, actualResponse);
            Assert.Equal((int)_response.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Post_Create_Option_Return_201() {
            var expectedResponse = new Response<OptionResponse>(HttpStatusCode.Created, new OptionResponse(
                _optionId,
                "Thường",
                10,
                10000,
                1000,
                true,
                DateTime.UtcNow
            ));

            _mockOptionsService
               .Setup(service => service.CreateAsync(_productId, _request))
               .ReturnsAsync(expectedResponse);

            var result = await _controller.Save(_productId, _request);

            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<OptionResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]
        public async Task Put_Update_Product_Return_200() {
            _mockOptionsService
              .Setup(service => service.UpdateAsync(_optionId, _productId, _request))
              .ReturnsAsync(_response);

            var result = await _controller.Update(_optionId, _optionId, _request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal((int)_response.Code, okResult.StatusCode!.Value);
        }

        [Fact]
        public async Task Delete_Product_Return_200() {
            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Option deleted successfully!");

            _mockOptionsService
            .Setup(service => service.DeleteAsync(_productId, _optionId))
            .ReturnsAsync(expectedResponse);


            var result = await _controller.Delete(_productId, _optionId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }
    }
}
