using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Product.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Queries.GetAll {
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, PaginationResponse<List<ProductResponse>>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IRepository<Domain.Models.Product> productRepository, IMapper mapper) {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken) {
            var totalItemsCount = await _productRepository.GetAll()
                                .CountAsync(x => !x.IsDeleted, cancellationToken);

            var totalPages = (int)Math.Ceiling(totalItemsCount / (double)request.Query.Limit);

            var products = await _productRepository.GetAll()
                                .Where(x => !x.IsDeleted)
                                .Include(c => c.Categories)
                                .Include(o => o.Options).Where(x => !x.IsDeleted)
                                .Skip((request.Query.Page - 1) * request.Query.Limit)
                                .Take(request.Query.Limit)
                                .OrderByDescending(x => x.CreateAt)
                                .ToArrayAsync(cancellationToken);

            var result = new PaginationResponse<List<ProductResponse>> {
                CurrentPage = request.Query.Page,
                TotalPage = totalPages,
                Items = request.Query.Limit,
                TotalItems = totalItemsCount,
                Result = _mapper.Map<List<ProductResponse>>(products)
            };

            return result;
        }
    }
}