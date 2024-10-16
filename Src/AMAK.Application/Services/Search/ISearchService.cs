using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Search.Dtos;

namespace AMAK.Application.Services.Search {
    public interface ISearchService {
        Task<Response<SearchResponse>> Search(SearchQuery searchQuery);
    }
}