using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Authentication.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Security.Claims;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class AuthenticationControllerTest {
        private readonly Mock<IAuthService> _mockAuthenticationService;
        private readonly AuthenticationController _authController;
        private readonly Mock<ClaimsPrincipal> _claimsPrincipal;

        public AuthenticationControllerTest() {
            _mockAuthenticationService = new Mock<IAuthService>();
            _claimsPrincipal = new Mock<ClaimsPrincipal>();
            _authController = new AuthenticationController(_mockAuthenticationService.Object) {
                ControllerContext = new ControllerContext() {
                    HttpContext = new DefaultHttpContext() {
                        User = _claimsPrincipal.Object
                    }
                }
            };
        }

        [Fact]

        public async Task Post_Register_Account_Return_201() {
            var expectedResponse = new Response<RegisterResponse>(HttpStatusCode.Created, new RegisterResponse(
                Guid.NewGuid().ToString(),
                "May",
                "Nguyen",
                "Avatar.jpg",
                DateTime.UtcNow
            ));

            var registerRequest = new RegisterRequest() {
                Email = "example@mail.com",
                FirstName = "May",
                LastName = "Nguyen",
                Password = "123456a@"
            };

            _mockAuthenticationService
                .Setup(service => service.RegisterAsync(registerRequest))
                .ReturnsAsync(expectedResponse);

            var result = await _authController.Register(registerRequest);


            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<RegisterResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Post_Login_Account_Return_200() {
            var expectedResponse = new Response<TokenResponse>(HttpStatusCode.OK, new TokenResponse() {
                AccessToken = "accessToken",
                RefreshToken = "refreshToken"
            });

            var requestLogin = new LoginRequest() {
                Email = "example@mail.com",
                Password = "123456a@"
            };

            _mockAuthenticationService
               .Setup(service => service.LoginAsync(requestLogin))
               .ReturnsAsync(expectedResponse);

            var result = await _authController.Login(requestLogin);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<TokenResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]

        public async Task Post_Refresh_Token_Return_200() {
            var expectedResponse = new Response<TokenResponse>(HttpStatusCode.OK, new TokenResponse() {
                AccessToken = "accessToken",
                RefreshToken = "refreshToken"
            });

            var request = new TokenRequest() {
                Token = "refreshToken"
            };

            _mockAuthenticationService
              .Setup(service => service.RefreshTokenAsync(request))
              .ReturnsAsync(expectedResponse);

            var result = await _authController.RefreshToken(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<TokenResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]

        public async Task Get_Verify_Account_Return_200() {
            var userId = Guid.NewGuid().ToString();
            var token = "token";

            var expectedResponse = new Response<TokenResponse>(HttpStatusCode.OK, new TokenResponse() {
                AccessToken = "accessToken",
                RefreshToken = "refreshToken"
            });

            _mockAuthenticationService
             .Setup(service => service.VerifyAccountAsync(userId, token))
             .ReturnsAsync(expectedResponse);

            var result = await _authController.VerifyAccount(userId, token);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<TokenResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }


        [Fact]
        public async Task Post_Reset_Password_Return_200() {
            var userId = Guid.NewGuid().ToString();
            var token = "token";

            var resetPasswordRequest = new ResetPasswordRequest() {
                NewPassword = "98765a@"
            };

            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Reset password successfully!");

            _mockAuthenticationService
                .Setup(service => service.ResetPasswordAsync(userId, token, resetPasswordRequest))
                .ReturnsAsync(expectedResponse);

            var result = await _authController.ResetPassword(userId, token, resetPasswordRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);

        }

        [Fact]
        public async Task Post_Forgot_Password_Return_200() {
            var forgotPasswordRequest = new ForgotPasswordRequest() {
                Email = "mail@example.com"
            };

            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Send mail reset password successfully!");

            _mockAuthenticationService
              .Setup(service => service.ForgotPasswordAsync(forgotPasswordRequest))
              .ReturnsAsync(expectedResponse);

            var result = await _authController.ForgotPassword(forgotPasswordRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);

        }

        [Fact]
        public async Task Post_Logout_Return_200() {
            var request = new TokenRequest() {
                Token = "accessToken"
            };

            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Logout successfully!");

            _mockAuthenticationService
                .Setup(service => service.LogoutAsync(_claimsPrincipal.Object, request))
                .ReturnsAsync(expectedResponse);

            var result = await _authController.Logout(request);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);
        }

        [Fact]

        public async Task Post_Login_With_Google_Return_200() {
            var expectedResponse = new Response<TokenResponse>(HttpStatusCode.OK, new TokenResponse() {
                AccessToken = "accessToken",
                RefreshToken = "refreshToken"
            });

            _mockAuthenticationService
             .Setup(service => service.SignInWithGoogle(It.IsAny<SocialLoginRequest>()))
             .ReturnsAsync(expectedResponse);

            var result = await _authController.LoginWithGoogle(It.IsAny<SocialLoginRequest>());

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<TokenResponse>>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.Equal((int)expectedResponse.Code, okResult.StatusCode!.Value);

        }
    }
}