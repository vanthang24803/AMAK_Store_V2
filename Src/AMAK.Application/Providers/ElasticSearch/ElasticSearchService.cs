using Nest;

namespace AMAK.Application.Providers.ElasticSearch {
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class {
        private readonly IElasticClient _client;
        private readonly string _indexName;

        public ElasticSearchService(IElasticClient client) {
            this._client = client;
            _indexName = typeof(T).Name.ToLower();
        }

        public async Task<bool> Delete(string id) {
            var response = await _client.DeleteAsync<T>(id, d => d.Index(_indexName));
            return response.IsValid;
        }

        public async Task<T> Get(string id) {
            var response = await _client.GetAsync<T>(id, g => g.Index(_indexName));
            return response.Source;
        }

        public async Task<IEnumerable<T>> GetAll(QueryContainer? query) {
            var response = await _client.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(_ => query ?? new MatchAllQuery())
            );

            if (!response.IsValid || response.Documents == null) {
                return [];
            }

            return response.Documents;
        }
        public async Task<IEnumerable<string>> Index(IEnumerable<T> documents) {
            var indexExistsResponse = await _client.Indices.ExistsAsync(_indexName);

            if (!indexExistsResponse.Exists) {
                await _client.Indices.CreateAsync(_indexName, c => c.Map<T>(m => m.AutoMap()));
            }

            var indexResponse = await _client.IndexManyAsync(documents, _indexName);
            return indexResponse.Items.Select(item => item.Id);
        }

        public async Task<bool> Update(T document, string id) {
            var response = await _client.UpdateAsync<T>(id, u => u
                .Index(_indexName)
                .Doc(document)
                .DocAsUpsert());
            return response.IsValid;
        }
    }

}