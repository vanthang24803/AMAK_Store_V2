
using Nest;

namespace AMAK.Application.Providers.ElasticSearch {
    public interface IElasticSearchService<T> {
        Task<IEnumerable<string>> Index(IEnumerable<T> documents);
        Task<T> Get(string id);
        Task<bool> Update(T document, string id);
        Task<bool> Delete(string id);
        Task<IEnumerable<T>> GetAll(QueryContainer? query = null);
    }
}