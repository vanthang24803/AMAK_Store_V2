using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Tickets.Dtos;

namespace AMAK.Application.Services.Tickets {
    public interface ITicketService {
        Task<Response<string>> CreateAsync(TicketSchema request);
        Task<Response<string>> UpdateAsync(Guid id, TicketSchema request);
        Task<Response<string>> DeleteAsync(Guid id);
        Task<Response<TicketResponse>> GetDetailAsync(Guid id);
        Task<PaginationResponse<List<TicketResponse>>> GetAllAsync(BaseQuery query);
        Task<Response<TicketResponse>> FindTicketByCodeAsync(FindTicketRequest request);
        Task CheckTicketJob();

    }
}