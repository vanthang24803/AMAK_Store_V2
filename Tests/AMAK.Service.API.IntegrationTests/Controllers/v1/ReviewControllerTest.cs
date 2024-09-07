using AMAK.API.Controllers.v1;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Review;
using AMAK.Application.Services.Review.Dtos;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Security.Claims;

namespace AMAK.Service.API.IntegrationTests.Controllers.v1 {
    public class ReviewControllerTest {
        private readonly Mock<IReviewService> _mockReviewService;
        private readonly ReviewsController _reviewController;
        private readonly Mock<ClaimsPrincipal> _claimsPrincipal;
        private readonly Mock<List<IFormFile>> _mockFiles;

        public ReviewControllerTest() {
            _mockFiles = new Mock<List<IFormFile>>();
            _mockReviewService = new Mock<IReviewService>();
            _claimsPrincipal = new Mock<ClaimsPrincipal>();
            _reviewController = new ReviewsController(_mockReviewService.Object) {
                ControllerContext = new ControllerContext() {
                    HttpContext = new DefaultHttpContext() {
                        User = _claimsPrincipal.Object
                    }
                }
            };
        }

        [Fact]
        public async Task Get_All_Reviews_For_Product_Return_200() {
            var productId = Guid.NewGuid();

            var expectedResponse = new ListReviewResponse<List<ReviewResponse>> {
                Result = [new()],
                Code = (int)HttpStatusCode.OK
            };

            _mockReviewService
                .Setup(service => service.GetAllAsync(productId, It.IsAny<ReviewQuery>()))
                .ReturnsAsync(expectedResponse);

            var result = await _reviewController.Get(productId, It.IsAny<ReviewQuery>());

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var actualResponse = Assert.IsType<ListReviewResponse<List<ReviewResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }


        [Fact]
        public async Task Get_All_Reviews_For_User_Return_200() {
            var productId = Guid.NewGuid();

            var expectedResponse = new ListReviewResponse<List<ReviewResponse>> {
                Result = [new()],
                Code = (int)HttpStatusCode.OK
            };

            _mockReviewService
                .Setup(service => service.GetAsync(_claimsPrincipal.Object, It.IsAny<ReviewQuery>()))
                .ReturnsAsync(expectedResponse);

            var result = await _reviewController.GetByUser(It.IsAny<ReviewQuery>());

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var actualResponse = Assert.IsType<ListReviewResponse<List<ReviewResponse>>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }



        [Fact]

        public async Task Get_One_Review_Return_200() {
            var productId = Guid.NewGuid();
            var reviewId = Guid.NewGuid();

            var expectedResponse = new Response<ReviewResponse>(HttpStatusCode.OK, new Faker<ReviewResponse>());

            _mockReviewService
               .Setup(service => service.GetOneAsync(productId))
               .ReturnsAsync(expectedResponse);

            var result = await _reviewController.GetOne(productId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var actualResponse = Assert.IsType<Response<ReviewResponse>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);

        }


        [Fact]

        public async Task Post_Create_Review_Return_201() {
            var expectedResponse = new Response<string>(HttpStatusCode.Created, "Review created!");
            _mockReviewService
              .Setup(service => service.CreateAsync(_claimsPrincipal.Object, It.IsAny<CreateReviewRequest>(), _mockFiles.Object))
              .ReturnsAsync(expectedResponse);

            var result = await _reviewController.Save(It.IsAny<CreateReviewRequest>(), _mockFiles.Object);

            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);

            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);
        }

        [Fact]

        public async Task Delete_Review_Return_200() {

            var reviewId = Guid.NewGuid();

            var expectedResponse = new Response<string>(HttpStatusCode.OK, "Review hidden successfully!");

            _mockReviewService
                    .Setup(service => service.RemoveAsync(reviewId))
                    .ReturnsAsync(expectedResponse);

            var result = await _reviewController.Remove(reviewId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<Response<string>>(okResult.Value);
            Assert.Equal(expectedResponse.Result, actualResponse.Result);
            Assert.Equal(expectedResponse.Code, actualResponse.Code);

        }


    }
}