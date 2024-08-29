using AMAK.Application.Providers.ElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace AMAK.API.Controllers.v1 {
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase {
        private readonly IElasticSearchService<ProductTest> _elasticsearchService;

        private readonly IElasticSearchService<Domain.Models.Product> _productSearch;

        public TestController(IElasticSearchService<ProductTest> elasticsearchService, IElasticSearchService<Domain.Models.Product> productSearch) {
            _elasticsearchService = elasticsearchService;
            _productSearch = productSearch;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IEnumerable<ProductTest> products) {
            var ids = await _elasticsearchService.Index(products);
            return Ok(ids);
        }

        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] string name) {
            QueryContainer? query = null;

            if (!string.IsNullOrEmpty(name)) {
                query = new QueryContainerDescriptor<ProductTest>()
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(name)
                        .Fuzziness(Fuzziness.Auto) 
                        .Operator(Operator.Or) 
                    );
            }

            return Ok(await _productSearch.GetAll(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(string id) {
            var product = await _productSearch.Get(id);
            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductTest product) {
            var result = await _elasticsearchService.Update(product, id);
            if (!result) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            var result = await _elasticsearchService.Delete(id);
            if (!result) {
                return NotFound();
            }
            return NoContent();
        }


        public class ProductTest {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Name { get; set; } = null!;
            public decimal Price { get; set; }
        }

        public class TestQuery {
            public string? name = null;
        }
    }

}