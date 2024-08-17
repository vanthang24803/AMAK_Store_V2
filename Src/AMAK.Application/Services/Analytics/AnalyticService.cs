using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace AMAK.Application.Services.Analytics {
    public class AnalyticService : IAnalyticService {

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        private readonly ICacheService _cacheService;

        private readonly Dictionary<string, EOrderStatus> status;

        public AnalyticService(IRepository<Domain.Models.Order> orderRepository, IRepository<Domain.Models.OrderDetail> orderDetailRepository, ICacheService cacheService) {
            _cacheService = cacheService;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            status = new Dictionary<string, EOrderStatus>
            {
                { "Pending" , EOrderStatus.PENDING },
                { "Create" , EOrderStatus.CREATE },
                { "Shipping" , EOrderStatus.SHIPPING },
                { "Success" , EOrderStatus.SUCCESS },
                {"Cancel" , EOrderStatus.CANCEL},
                {"Return" , EOrderStatus.RETURN}
            };
        }

        public async Task<Response<Dictionary<string, double>>> GetBarChartAsync() {

            var orders = await _orderRepository.GetAll()
                .Where(x => x.Status == EOrderStatus.SUCCESS)
                .ToListAsync();

            var monthlyRevenue = new Dictionary<string, double>
           {
                {"Jan", 0}, {"Feb", 0}, {"Mar", 0}, {"Apr", 0},
                {"May", 0}, {"Jun", 0}, {"Jul", 0}, {"Aug", 0},
                {"Sep", 0}, {"Oct", 0}, {"Nov", 0}, {"Dec", 0}
            };

            foreach (var order in orders) {
                var month = order.CreateAt.ToString("MMM");
                monthlyRevenue[month] += order.TotalPrice;
            }

            return new Response<Dictionary<string, double>>(HttpStatusCode.OK, monthlyRevenue);
        }

        public async Task<Response<StatisticalResponse>> GetStatisticalAsync(AnalyticQuery query) {

            var cacheKey = $"Statistical_{query.StartAt}_{query.EndAt}_{query.Status}__{query.Page}_{query.Limit}";

            var cachedData = await _cacheService.GetData<Response<StatisticalResponse>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }


            EOrderStatus? filterStatus = null;
            if (!string.IsNullOrWhiteSpace(query.Status) && status.TryGetValue(query.Status, out EOrderStatus value)) {
                filterStatus = value;
            }

            var orderQuery = _orderRepository.GetAll().AsQueryable();

            if (filterStatus.HasValue) {
                orderQuery = orderQuery.Where(o => o.Status == filterStatus.Value);
            }

            var culture = new CultureInfo("vi-VN");
            var dateFormat = "d/M/yyyy";

            if (!string.IsNullOrWhiteSpace(query.StartAt) && DateTime.TryParseExact(query.StartAt, dateFormat, culture, DateTimeStyles.None, out DateTime startAt)) {
                startAt = DateTime.SpecifyKind(startAt, DateTimeKind.Utc);
                orderQuery = orderQuery.Where(o => o.CreateAt >= startAt);
            }
            if (!string.IsNullOrWhiteSpace(query.EndAt) && DateTime.TryParseExact(query.EndAt, dateFormat, culture, DateTimeStyles.None, out DateTime endAt)) {
                endAt = DateTime.SpecifyKind(endAt, DateTimeKind.Utc);
                orderQuery = orderQuery.Where(o => o.CreateAt <= endAt);
            }

            // Pagination
            var totalCount = await orderQuery.CountAsync();
            var skip = (query.Page - 1) * query.Limit;
            orderQuery = orderQuery.Skip(skip).Take(query.Limit);

            var totalOrder = await orderQuery.CountAsync();
            var totalPrice = await orderQuery.SumAsync(x => x.TotalPrice);

            var orderDetailsQuery = from order in orderQuery
                                    from detail in _orderDetailRepository.GetAll()
                                    where order.Id == detail.OrderId
                                    select new OrderDetailResponse {
                                        ProductId = detail.ProductId,
                                        Name = detail.ProductName,
                                        OptionName = detail.OptionName,
                                        Price = detail.Price,
                                        Quantity = detail.Quantity,
                                        Sale = detail.Sale,
                                        Thumbnail = detail.Thumbnail
                                    };

            var orderDetailsGroupedByOrderId = await orderDetailsQuery
                                               .ToListAsync()
                                               .ContinueWith(task => task.Result.GroupBy(d => d.ProductId).ToDictionary(g => g.Key, g => g.ToList()));

            var orderResponseList = await orderQuery.Select(order => new StatisticalOrderResponse {
                Id = order.Id,
                Customer = order.Customer,
                Email = order.Email,
                Status = order.Status,
                Address = order.Address,
                NumberPhone = order.NumberPhone!,
                Payment = order.Payment,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                CreateAt = order.CreateAt,
            }).ToListAsync();

            var totalSold = orderDetailsGroupedByOrderId.Sum(group => group.Value.Sum(detail => detail.Quantity));

            var totalPage = (int)Math.Ceiling((double)totalCount / query.Limit);

            var response = new StatisticalResponse {
                StartAt = query.StartAt,
                EndAt = query.EndAt,
                Limit = query.Limit,
                CurrentPage = query.Page,
                TotalPage = totalPage,
                TotalItems = totalCount,
                TotalOrder = totalOrder,
                TotalPrice = totalPrice,
                TotalSold = totalSold,
                Orders = orderResponseList
            };


            var result = new Response<StatisticalResponse>(HttpStatusCode.OK, response);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(5));

            return result;
        }
    }
}