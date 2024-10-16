using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Photo.Dtos;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Search.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Search {
    public class SearchService : ISearchService {

        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Order> _orderRepository;

        public SearchService(IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.Order> orderRepository) {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }


        public async Task<Response<SearchResponse>> Search(SearchQuery searchQuery) {
            var searchTerm = searchQuery.Name != null ? $"%{searchQuery.Name}%" : "%";

            var products = await _productRepository.GetAll()
                .Include(x => x.Options)
                .Where(p => EF.Functions.Like(p.Name, searchTerm) ||
                            p.Options.Any(o => EF.Functions.Like(o.Name, searchTerm)))
                .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Brand,
                p.Thumbnail ?? "",
                p.Sold,
                p.Categories.Select(c => new CategoryResponse(c.Id, c.Name, c.CreateAt)).ToList(),
                p.Options.Select(o => new OptionResponse(o.Id, o.Name, o.Sale, o.Price, o.Quantity, o.IsActive, o.CreateAt)).ToList(),
                p.Photos.Select(ph => new PhotoResponse(ph.Id, ph.Url, ph.CreateAt)).ToList(),
                p.CreateAt))
                    .ToListAsync();

            var orders = await _orderRepository.GetAll()
                .Where(o => EF.Functions.Like(o.Customer, searchTerm))
                .Select(o => new {
                    Order = o,
                    LatestStatus = o.Status.OrderByDescending(s => s.TimeStamp).FirstOrDefault()
                })
                .ToListAsync();

            var orderResponses = orders.Select(o => new OrderResponse {
                Id = o.Order.Id,
                Customer = o.Order.Customer,
                Email = o.Order.Email,
                Address = o.Order.Address,
                IsReviewed = o.Order.IsReviewed,
                NumberPhone = o.Order.NumberPhone ?? "",
                Payment = o.Order.Payment,
                Status = o.LatestStatus?.Status ?? Domain.Enums.EOrderStatus.PENDING,
                Quantity = o.Order.Quantity,
                TotalPrice = o.Order.TotalPrice,
                OrderDetails = [],
                StatusOrders = o.Order.Status.Select(s => new StatusOrder {
                    Status = s.Status,
                    Timestamp = s.TimeStamp
                }).ToList(),
                CreateAt = o.Order.CreateAt,
                UpdateAt = o.Order.UpdateAt
            }).ToList();


            return new Response<SearchResponse>(System.Net.HttpStatusCode.OK, new SearchResponse() {
                Orders = orderResponses,
                Products = products
            });
        }
    }
}