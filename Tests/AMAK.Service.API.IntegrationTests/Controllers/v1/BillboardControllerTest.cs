using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Billboard;
using AMAK.Application.Services.Billboard.Dtos;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class BillboardControllerTest {
        private readonly Mock<IBillboardService> _billboardServiceMock;

        private readonly BillboardsController _billboardsController;

        public BillboardControllerTest() {
            _billboardServiceMock = new Mock<IBillboardService>();
            _billboardsController = new BillboardsController(_billboardServiceMock.Object);
        }


        [Fact]
        public async Task Get_GetAll_Billboard_Return_200() {
            var expectedResponse = new Response<List<BillboardResponse>>(HttpStatusCode.OK, new Faker<List<BillboardResponse>>());

            _billboardServiceMock
                 .Setup(service => service.GetAllAsync())
                 .ReturnsAsync(expectedResponse);


            var result = await _billboardsController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<List<BillboardResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }

        [Fact]
        public async Task Delete_One_Billboard_Return_200() {
            var billboardId = Guid.NewGuid();

            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Billboard deleted successfully!");

            _billboardServiceMock
                .Setup(service => service.DeleteAsync(billboardId))
                .ReturnsAsync(expectedResponse);

            var result = await _billboardsController.Delete(billboardId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }


        [Fact]

        public async Task Post_Create_One_Billboard_Return_201() {
            var expectedResponse = new Response<BillboardResponse>(HttpStatusCode.Created, It.IsAny<BillboardResponse>());


            _billboardServiceMock
             .Setup(service => service.CreateAsync(It.IsAny<IFormFile>(), It.IsAny<CreateBillboardRequest>()))
             .ReturnsAsync(expectedResponse);


            var result = await _billboardsController.Create(It.IsAny<IFormFile>(),It.IsAny<CreateBillboardRequest>());

            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<BillboardResponse>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }

    }

}