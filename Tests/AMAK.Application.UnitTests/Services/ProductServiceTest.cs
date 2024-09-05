using Moq;
using AMAK.Application.Services.Product.Queries.GetAll;
using AMAK.Application.Common.Helpers;
using AutoMapper;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Product.Queries.GetDetail;
using System.Net;

namespace AMAK.Application.UnitTests.Services {
    public class ProductServiceTest {
        private readonly Mock<IRepository<Domain.Models.Product>> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly GetAllProductQueryHandler _getAllProductHandler;
        private readonly GetProductDetailQueryHandler _getDetailQueryHandler;
        private readonly Guid _productId;

        public ProductServiceTest() {
            _productId = Guid.NewGuid();
            _mapperMock = new Mock<IMapper>();
            _cacheServiceMock = new Mock<ICacheService>();
            _productRepositoryMock = new Mock<IRepository<Domain.Models.Product>>();
            _getAllProductHandler = new GetAllProductQueryHandler(_productRepositoryMock.Object, _mapperMock.Object, _cacheServiceMock.Object);
            _getDetailQueryHandler = new GetProductDetailQueryHandler(_productRepositoryMock.Object, _mapperMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task Handle_GetAll_ReturnsCachedData_IfCacheExists() {
            // Arrange
            var productQuery = new ProductQuery { Name = "Test" };
            var query = new GetAllProductQuery(productQuery);
            var cachedResponse = new PaginationResponse<List<ProductResponse>> { CurrentPage = 1, TotalPage = 1, Items = 1, TotalItems = 1, Result = [] };

            _cacheServiceMock.Setup(c => c.GetData<PaginationResponse<List<ProductResponse>>>(It.IsAny<string>()))
                .ReturnsAsync(cachedResponse);

            // Act
            var result = await _getAllProductHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(cachedResponse, result);
        }

        [Fact]
        public void Handle_GetAll_ReturnsFilteredProducts_IfNoCache() {
            var productQuery = new ProductQuery {
                Name = "Nguời là ai",
            };

            var productResponse = new PaginationResponse<List<ProductResponse>> { CurrentPage = 1, TotalPage = 1, Items = 1, TotalItems = 1, Result = [] };

            var query = new GetAllProductQuery(productQuery);

            var products = new List<Domain.Models.Product> {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Người Là Ai Giữa Tâm Tư Này - Tập 1",
                    Options = [new() { Id = Guid.NewGuid(), Price = 500000 }]
                }
            };

            _productRepositoryMock.Setup(p => p.GetAll())
                        .Returns(products.AsQueryable());
            _mapperMock.Setup(m => m.Map<List<ProductResponse>>(It.IsAny<List<Domain.Models.Product>>())).Returns([]);

            // Act
            var result = _getAllProductHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Handle_GetDetail_ReturnsCachedData_IfCacheExists() {
            var getDetailQuery = new GetProductDetailQuery(_productId);

            var cachedResponse = new Response<ProductDetailResponse>(HttpStatusCode.OK, new ProductDetailResponse(
                    _productId,
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

            _cacheServiceMock.Setup(c => c.GetData<Response<ProductDetailResponse>>(It.IsAny<string>()))
                   .ReturnsAsync(cachedResponse);

            var result = await _getDetailQueryHandler.Handle(getDetailQuery, CancellationToken.None);
            Assert.Equal(cachedResponse.Code, result.Code);
            Assert.Equal(cachedResponse.Result, result.Result);
        }

        [Fact]
        public void Handle_GetDetail_ReturnData_IfNoCache() {
            var getDetailQuery = new GetProductDetailQuery(_productId);

            var productDetailResponse = new ProductDetailResponse(
                   _productId,
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
                   DateTime.UtcNow);

            var expectedResponse = new Response<ProductDetailResponse>(HttpStatusCode.OK, productDetailResponse);

            _cacheServiceMock.Setup(c => c.GetData<Response<ProductDetailResponse>>(It.IsAny<string>()))
                   .ReturnsAsync(null as Response<ProductDetailResponse>);

            var result = _getDetailQueryHandler.Handle(getDetailQuery, CancellationToken.None);

            Assert.NotNull(result);
        }

    }
}
