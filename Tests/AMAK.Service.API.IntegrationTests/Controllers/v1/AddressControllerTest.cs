
using System.Security.Claims;
using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Address;
using AMAK.Application.Services.Address.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using AMAK.Application.Common.Query;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class AddressControllerTest {
        private readonly Mock<IAddressService> _mockAddressService;

        private readonly AddressesController _addressesController;

        private readonly Mock<ClaimsPrincipal> _claimsPrincipal;

        public AddressControllerTest() {
            _claimsPrincipal = new Mock<ClaimsPrincipal>();
            _mockAddressService = new Mock<IAddressService>();
            _addressesController = new AddressesController(_mockAddressService.Object) {
                ControllerContext = new ControllerContext() {
                    HttpContext = new DefaultHttpContext() {
                        User = _claimsPrincipal.Object
                    }
                }
            };
        }

        [Fact]

        public async Task Get_All_Address_Return_200() {
            var expectedResponse = new PaginationResponse<List<AddressResponse>>() {
                Result = It.IsAny<List<AddressResponse>>()
            };

            _mockAddressService
                .Setup(service => service.GetAddressesAsync(_claimsPrincipal.Object, It.IsAny<BaseQuery>()))
                .ReturnsAsync(expectedResponse);

            var result = await _addressesController.GetAll(It.IsAny<BaseQuery>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<PaginationResponse<List<AddressResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal(expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Post_Create_Address_Return_201() {
            var expectedResponse = new Response<AddressResponse>(HttpStatusCode.Created, It.IsAny<AddressResponse>());

            _mockAddressService
                .Setup(service => service.CreateAddressAsync(_claimsPrincipal.Object, It.IsAny<AddressRequest>()))
                .ReturnsAsync(expectedResponse);

            var result = await _addressesController.Create(It.IsAny<AddressRequest>());

            var okResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<AddressResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Put_Create_Address_Return_200() {
            var expectedResponse = new Response<AddressResponse>(HttpStatusCode.OK, It.IsAny<AddressResponse>());

            _mockAddressService
                .Setup(service => service.UpdateAddressAsync(_claimsPrincipal.Object, It.IsAny<Guid>(), It.IsAny<AddressRequest>()))
                .ReturnsAsync(expectedResponse);

            var result = await _addressesController.Update(It.IsAny<Guid>(), It.IsAny<AddressRequest>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<AddressResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Delete_Create_Address_Return_200() {
            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Address deleted successfully!");

            _mockAddressService
                .Setup(service => service.RemoveAddressAsync(_claimsPrincipal.Object, It.IsAny<Guid>()))
                .ReturnsAsync(expectedResponse);

            var result = await _addressesController.Remove(It.IsAny<Guid>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }
    }
}