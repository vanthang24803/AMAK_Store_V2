using System.Net;
using System.Security.Claims;
using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Me;
using AMAK.Application.Services.Me.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class MeControllerTest {
        private readonly Mock<IMeService> _mockMeService;
        private readonly MeController _meController;
        private readonly Mock<ClaimsPrincipal> _claimsPrincipal;
        private readonly Mock<IFormFile> _mockFile;

        public MeControllerTest() {
            _mockFile = new Mock<IFormFile>();
            _mockMeService = new Mock<IMeService>();
            _claimsPrincipal = new Mock<ClaimsPrincipal>();
            _meController = new MeController(_mockMeService.Object) {
                ControllerContext = new ControllerContext() {
                    HttpContext = new DefaultHttpContext() {
                        User = _claimsPrincipal.Object
                    }
                }
            };
        }

        [Fact]

        public async Task Get_Profile_Return_200() {
            var expectedResponse = new Response<ProfileResponse>(HttpStatusCode.OK, It.IsAny<ProfileResponse>());

            _mockMeService
              .Setup(service => service.GetProfileAsync(_claimsPrincipal.Object))
              .ReturnsAsync(expectedResponse);

            var result = await _meController.GetProfile();

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<ProfileResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]
        public async Task Put_Update_Profile_Return_200() {
            var expectedResponse = new Response<ProfileResponse>(HttpStatusCode.OK, It.IsAny<ProfileResponse>());

            _mockMeService
             .Setup(service => service.UpdateProfileAsync(_claimsPrincipal.Object, It.IsAny<UpdateProfileRequest>()))
             .ReturnsAsync(expectedResponse);

            var result = await _meController.UpdateProfile(It.IsAny<UpdateProfileRequest>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<ProfileResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]

        public async Task Put_Update_Avatar_Return_200() {
            var expectedResponse = new Response<ProfileResponse>(HttpStatusCode.OK, It.IsAny<ProfileResponse>());

            _mockMeService
             .Setup(service => service.UploadAvatarAsync(_claimsPrincipal.Object, _mockFile.Object))
             .ReturnsAsync(expectedResponse);

            var result = await _meController.UploadAvatar(_mockFile.Object);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<ProfileResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]

        public async Task Put_Update_Password_Return_200() {
            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Password updated successfully!");

            _mockMeService
                .Setup(service => service.UpdatePasswordAsync(_claimsPrincipal.Object, It.IsAny<UpdatePasswordRequest>()))
                .ReturnsAsync(expectedResponse);

            var result = await _meController.UploadPassword(It.IsAny<UpdatePasswordRequest>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

    }
}