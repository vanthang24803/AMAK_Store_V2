using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Options;
using AMAK.Application.Services.Options.Dtos;
using AutoMapper;
using Moq;
using System.Net;


namespace AMAK.Application.UnitTests.Services {
    public class OptionServiceTest {
        private readonly Mock<IRepository<Domain.Models.Option>> _optionRepository;
        private readonly Mock<IRepository<Domain.Models.Product>> _productRepository;

        private readonly OptionService _optionService;
        private readonly Mock<IMapper> _mockMapper;

        public OptionServiceTest() {
            _optionRepository = new Mock<IRepository<Domain.Models.Option>>();
            _productRepository = new Mock<IRepository<Domain.Models.Product>>();
            Mock<ICacheService> cacheService = new();
            _mockMapper = new Mock<IMapper>();

            _optionService = new OptionService(
                _optionRepository.Object,
                _productRepository.Object,
                _mockMapper.Object,
                cacheService.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedResponse_WhenProductExists() {
            // Arrange
            var productId = Guid.NewGuid();
            var request = new OptionRequest();
            var product = new Domain.Models.Product { Id = productId };
            var newOption = new Domain.Models.Option() {
                Id = productId,
            };
            var optionResponse = new OptionResponse(
                productId,
                "Thường",
                12,
                100000,
                1000,
                true,
                DateTime.UtcNow
            );

            // Setup mocks
            _productRepository.Setup(x => x.GetById(productId)).ReturnsAsync(product);
            _mockMapper.Setup(x => x.Map<Domain.Models.Option>(request)).Returns(newOption);
            _mockMapper.Setup(x => x.Map<OptionResponse>(newOption)).Returns(optionResponse);

            // Act
            var response = await _optionService.CreateAsync(productId, request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.Equal(optionResponse, response.Result);
            _optionRepository.Verify(x => x.Add(newOption), Times.Once);
            _optionRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
