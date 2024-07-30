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
        private readonly Dictionary<string, (int, int?)> priceLevels;


        public GetAllProductQueryHandler(IRepository<Domain.Models.Product> productRepository, IMapper mapper) {
            _productRepository = productRepository;
            _mapper = mapper;

            priceLevels = new Dictionary<string, (int, int?)>{
                { "Max", (400000, null) },
                { "Highest", (300000, 400000) },
                { "High", (200000, 300000) },
                { "Medium", (100000, 200000) },
                { "Low", (0, 100000) }
            };
        }

        public async Task<PaginationResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken) {
            var query = request.Query;

            var filteredProductsQuery = _productRepository.GetAll()
                .Where(x => !x.IsDeleted)
                .Include(c => c.Categories)
                .Include(o => o.Options)
                .Include(p => p.Photos)
                .AsQueryable();

            // TODO: Search By Name
            if (!string.IsNullOrWhiteSpace(query.Name)) {
                filteredProductsQuery = filteredProductsQuery
                        .Where(n => n.Name.ToLower().Contains(query.Name.ToLower()));
            }

            // TODO: Search By Category
            if (!string.IsNullOrEmpty(query.Category)) {
                filteredProductsQuery = filteredProductsQuery.Where(c => c.Categories.Any(cat => cat.Name == query.Category));
            }

            // TODO: SortBy
            if (!string.IsNullOrEmpty(query.SortBy) && priceLevels.TryGetValue(query.SortBy, out (int, int?) value)) {
                var (minPrice, maxPrice) = value;
                if (maxPrice.HasValue) {
                    filteredProductsQuery = filteredProductsQuery.Where(p => p.Options.Any(o => o.Price >= minPrice && o.Price <= maxPrice));
                } else {
                    filteredProductsQuery = filteredProductsQuery.Where(p => p.Options.Any(o => o.Price > minPrice));
                }
            }

            // TODO: Action
            if (!string.IsNullOrEmpty(query.Action)) {
                if (query.Action == "Top-selling") {
                    filteredProductsQuery = filteredProductsQuery.OrderByDescending(p => p.Sold);
                }
                if (query.Action == "OnSale") {
                    filteredProductsQuery = filteredProductsQuery
                        .Where(p => p.Options.Any(o => o.Sale > 10));
                }
            }

            //  TODO: OrderBy
            if (!string.IsNullOrEmpty(query.OrderBy)) {
                filteredProductsQuery = query.OrderBy switch {
                    "Alphabet" => filteredProductsQuery.OrderBy(n => n.Name),
                    "ReverseAlphabet" => filteredProductsQuery.OrderByDescending(n => n.Name),
                    "HighToLow" => filteredProductsQuery.OrderByDescending(o => o.Options.Max(p => p.Price)),
                    "LowToHigh" => filteredProductsQuery.OrderBy(o => o.Options.Min(p => p.Price)),
                    "Oldest" => filteredProductsQuery.OrderBy(p => p.CreateAt),
                    "Lasted" => filteredProductsQuery.OrderByDescending(p => p.CreateAt),
                    _ => filteredProductsQuery.OrderByDescending(p => p.CreateAt),
                };
            }

            // TODO: Pagination

            var totalItemsCount = await filteredProductsQuery.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalItemsCount / (double)query.Limit);

            var products = await filteredProductsQuery
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync(cancellationToken);

            var result = new PaginationResponse<List<ProductResponse>> {
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalItemsCount,
                Result = _mapper.Map<List<ProductResponse>>(products)
            };

            return result;
        }
    }
}