using Nest;

namespace AMAK.Application.Providers.ElasticSearch {
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class {
        private readonly IElasticClient client;
        private readonly string indexName;

        public ElasticSearchService(IElasticClient client) {
            this.client = client;
            this.indexName = typeof(T).Name.ToLower();
        }

        public async Task<bool> Delete(string id) {
            var response = await client.DeleteAsync<T>(id, d => d.Index(indexName));
            return response.IsValid;
        }

        public async Task<T> Get(string id) {
            var response = await client.GetAsync<T>(id, g => g.Index(indexName));
            return response.Source;
        }

        public async Task<IEnumerable<T>> GetAll(QueryContainer? query) {
            var response = await client.SearchAsync<T>(s => s
                .Index(indexName)
                 .Query(q => query ?? new MatchAllQuery())
            );

            if (!response.IsValid || response.Documents == null) {
                return [];
            }

            return response.Documents;
        }
        public async Task<IEnumerable<string>> Index(IEnumerable<T> documents) {
            var indexExistsResponse = await client.Indices.ExistsAsync(indexName);

            if (!indexExistsResponse.Exists) {
                await client.Indices.CreateAsync(indexName, c => c.Map<T>(m => m.AutoMap()));
            }

            var indexResponse = await client.IndexManyAsync(documents, indexName);
            return indexResponse.Items.Select(item => item.Id);
        }

        public async Task<bool> Update(T document, string id) {
            var response = await client.UpdateAsync<T>(id, u => u
                .Index(indexName)
                .Doc(document)
                .DocAsUpsert(true));
            return response.IsValid;
        }
    }

}