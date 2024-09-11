using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace AMAK.Application.Services.Analytics {
    public class AnalyticService : IAnalyticService {

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly ICacheService _cacheService;

        private readonly Dictionary<string, EOrderStatus> status;

        private readonly Dictionary<(double, double?), string> rank;


        public AnalyticService(IRepository<Domain.Models.Order> orderRepository, IRepository<Domain.Models.OrderDetail> orderDetailRepository, ICacheService cacheService, UserManager<ApplicationUser> userManager, IRepository<Category> categoryRepository, IRepository<Domain.Models.Product> productRepository) {
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
            rank = new Dictionary<(double, double?), string>()
         {
                { (0 , 100000), "Bronze" },
                { (100000, 500000),"Silver" },
                { (500000 , 1500000),"Gold" },
                { (1500000 , 3500000),"Platinum" },
                { (3500000, null),"Diamond" }
            };
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

       

        public async Task<Response<BarChartResponse>> GetBarChartAsync() {
            var cacheKey = $"Analytics_BarChart";

            var cachedData = await _cacheService.GetData<Response<BarChartResponse>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }
            var orders = await _orderRepository.GetAll()
                .Where(x => x.Status == EOrderStatus.SUCCESS)
                .ToListAsync();

            var monthlyRevenue = new Dictionary<string, double>
            {
                {"Jan", 0}, {"Feb", 0}, {"Mar", 0}, {"Apr", 0},
                {"May", 0}, {"Jun", 0}, {"Jul", 0}, {"Aug", 0},
                {"Sep", 0}, {"Oct", 0}, {"Nov", 0}, {"Dec", 0}
            };

            var dailyRevenue = new Dictionary<string, double>();
            var weeklyRevenue = new Dictionary<string, double>();

            DateTime today = DateTime.UtcNow;
            DateTime startOfLast30Days = today.AddDays(-30).Date;
            DateTime startOfLast7Days = today.AddDays(-7).Date;

            foreach (var order in orders) {
                var orderDate = order.CreateAt.Date;

                string month = order.CreateAt.ToString("MMM", CultureInfo.InvariantCulture);
                monthlyRevenue[month] += order.TotalPrice;

                if (orderDate >= startOfLast30Days && orderDate <= today) {
                    string dayKey = orderDate.ToString("yyyy-MM-dd");
                    if (!dailyRevenue.ContainsKey(dayKey)) {
                        dailyRevenue[dayKey] = 0;
                    }
                    dailyRevenue[dayKey] += order.TotalPrice;
                }

                if (orderDate >= startOfLast7Days && orderDate <= today) {
                    string weekKey = orderDate.ToString("yyyy-MM-dd");
                    if (!weeklyRevenue.ContainsKey(weekKey)) {
                        weeklyRevenue[weekKey] = 0;
                    }
                    weeklyRevenue[weekKey] += order.TotalPrice;
                }
            }

            for (DateTime date = startOfLast30Days; date <= today; date = date.AddDays(1)) {
                string dayKey = date.ToString("yyyy-MM-dd");
                if (!dailyRevenue.ContainsKey(dayKey)) {
                    dailyRevenue[dayKey] = 0;
                }
            }

            for (DateTime date = startOfLast7Days; date <= today; date = date.AddDays(1)) {
                string weekKey = date.ToString("yyyy-MM-dd");
                if (!weeklyRevenue.ContainsKey(weekKey)) {
                    weeklyRevenue[weekKey] = 0;
                }
            }

            var newBarChart = new BarChartResponse {
                Year = monthlyRevenue,
                Week = weeklyRevenue,
                Month = dailyRevenue,
            };

            var result = new Response<BarChartResponse>(HttpStatusCode.OK, newBarChart);

             await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));
             return result;
        }

        public async Task<Response<AnalyticCountResponse>> GetCountResponseAsync() {

            var customers = await _userManager.GetUsersInRoleAsync(Role.CUSTOMER);

            var orders = await _orderRepository.GetAll().Where(x => x.Status != EOrderStatus.SUCCESS).CountAsync();

            var productArchive = await _productRepository.GetAll().Where(x => x.IsDeleted).CountAsync();
            var productActive = await _productRepository.GetAll().Where(x => !x.IsDeleted).CountAsync();

            var categories = await _categoryRepository.GetAll().CountAsync();

            var response = new AnalyticCountResponse {
                Archive = productArchive,
                Active = productActive,
                Categories = categories,
                Orders = orders,
                Customers = customers.Count
            };

            return new Response<AnalyticCountResponse>(HttpStatusCode.OK, response);
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

            var orderQuery = _orderRepository.GetAll().AsSplitQuery();

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

        public async Task<Response<List<AnalyticsUserResponse>>> GetAnalyticsUserAsync() {
            var cacheKey = $"Analytics_Accounts";

            var cachedData = await _cacheService.GetData<Response<List<AnalyticsUserResponse>>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }


            var users = await _userManager.Users.ToListAsync();
            var analyticsUserResponseList = new List<AnalyticsUserResponse>();

            foreach (var user in users) {
                var totalPrice = await _orderRepository.GetAll()
                   .Where(x => x.UserId == user.Id && !x.IsDeleted && x.Status.Equals(EOrderStatus.SUCCESS))
                   .SumAsync(x => x.TotalPrice);

                var roles = await _userManager.GetRolesAsync(user);

                var isAdmin = roles.Contains(Role.ADMIN);
                var isManager = roles.Contains(Role.MANAGER);

                var analyticsUserResponse = new AnalyticsUserResponse {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email!,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Avatar = user.Avatar,
                    IsAdmin = isAdmin,
                    IsManager = isManager,
                    Rank = GetRank(totalPrice),
                    CreateAt = user.CreateAt,
                    UpdateAt = user.UpdateAt
                };

                analyticsUserResponseList.Add(analyticsUserResponse);
            }

            var result = new Response<List<AnalyticsUserResponse>>(HttpStatusCode.OK, analyticsUserResponseList);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));

            return result;
        }

        private string GetRank(double totalPrice) {
            var rankThreshold = rank.Keys.LastOrDefault(k => k.Item1 <= totalPrice && (k.Item2 == null || k.Item2 >= totalPrice));

            if (!rankThreshold.Equals(default)) {
                return rank[rankThreshold];
            } else {
                return string.Empty;
            }
        }
    }
}