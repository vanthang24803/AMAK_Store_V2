using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Tickets.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Tickets {
    public class TicketService : ITicketService {

        private readonly IRepository<Domain.Models.Voucher> _voucherRepository;
        private readonly ICacheService _cacheService;

        private readonly IMapper _mapper;

        public TicketService(IRepository<Voucher> voucherRepository, ICacheService cacheService, IMapper mapper) {
            _voucherRepository = voucherRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAsync(TicketSchema request) {
            var newTicket = _mapper.Map<Voucher>(request);

            _voucherRepository.Add(newTicket);

            await _voucherRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.Created, "Created ticket successfully!");
        }

        public async Task<Response<string>> DeleteAsync(Guid id) {
            var existingTicket = await _voucherRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException("Ticket not found");

            existingTicket.IsDeleted = true;

            _voucherRepository.Update(existingTicket);

            await _voucherRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Deleted ticket successfully!");
        }

        public async Task<Response<TicketResponse>> FindTicketByCodeAsync(FindTicketRequest request) {
            var existingTicket = await _voucherRepository.GetAll()
             .FirstOrDefaultAsync(x => x.Code == request.Code && !x.IsDeleted)
             ?? throw new NotFoundException("Ticket not found");

            return new Response<TicketResponse>(HttpStatusCode.OK, _mapper.Map<TicketResponse>(existingTicket));
        }

        public async Task<PaginationResponse<List<TicketResponse>>> GetAllAsync(BaseQuery query) {

            var totalRecords = await _voucherRepository.GetAll()
               .CountAsync(x => !x.IsDeleted);

            var totalPages = (int)Math.Ceiling(totalRecords / (double)query.Limit);


            var items = await _voucherRepository.GetAll()
                   .Where(x => !x.IsDeleted)
                   .Skip((query.Page - 1) * query.Limit)
                   .Take(query.Limit)
                   .ToListAsync();

            var mappedItems = _mapper.Map<List<TicketResponse>>(items);

            var result = new PaginationResponse<List<TicketResponse>> {
                Result = mappedItems,
                Code = 200,
                TotalItems = totalRecords,
                CurrentPage = query.Page,
                Items = query.Limit,
                TotalPage = totalPages

            };

            return result;
        }

        public async Task<Response<TicketResponse>> GetDetailAsync(Guid id) {
            var existingTicket = await _voucherRepository.GetAll()
             .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
             ?? throw new NotFoundException("Ticket not found");

            return new Response<TicketResponse>(HttpStatusCode.OK, _mapper.Map<TicketResponse>(existingTicket));
        }

        public async Task<Response<string>> UpdateAsync(Guid id, TicketSchema request) {
            var existingTicket = await _voucherRepository.GetAll()
              .FirstOrDefaultAsync(x => x.Id == id)
              ?? throw new NotFoundException("Ticket not found");

            _mapper.Map(request, existingTicket);

            _voucherRepository.Update(existingTicket);

            await _voucherRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Updated ticket successfully!");
        }
    }
}