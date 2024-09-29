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

        private readonly IRepository<Domain.Models.Option> _optionRepository;

        private readonly ICacheService _cacheService;

        private readonly Dictionary<string, EOrderStatus> status;


        private static readonly Dictionary<(double, double?), string> rank = new()
         {
                { (0 , 100000), "Bronze" },
                { (100000, 500000),"Silver" },
                { (500000 , 1500000),"Gold" },
                { (1500000 , 3500000),"Platinum" },
                { (3500000, null),"Diamond" }
            };


        public AnalyticService(IRepository<Domain.Models.Order> orderRepository, IRepository<Domain.Models.OrderDetail> orderDetailRepository, ICacheService cacheService, UserManager<ApplicationUser> userManager, IRepository<Category> categoryRepository, IRepository<Domain.Models.Product> productRepository, IRepository<Option> optionRepository) {
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
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _optionRepository = optionRepository;
        }
        public async Task<Response<BarChartResponse>> GetBarChartAsync() {
            var cacheKey = $"Analytics_BarChart";

            var cachedData = await _cacheService.GetData<Response<BarChartResponse>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }

            var orders = await _orderRepository.GetAll()
                 .Include(s => s.Status)
                 .Where(o => o.Status.All(st => st.Status != EOrderStatus.CANCEL))
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

        public async Task<Response<AreaChartResponse>> GetAreaChartAsync() {
            var cacheKey = $"Analytics_AreaChart";

            var cachedData = await _cacheService.GetData<Response<AreaChartResponse>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }

            DateTime today = DateTime.UtcNow;
            DateTime startOfLast30Days = today.AddDays(-30).Date;
            DateTime startOfLast7Days = today.AddDays(-7).Date;

            List<DateTime> daysInMonth = Enumerable.Range(0, (today - startOfLast30Days).Days + 1)
                .Select(offset => startOfLast30Days.AddDays(offset))
                .ToList();

            List<DateTime> daysInWeek = Enumerable.Range(0, (today - startOfLast7Days).Days + 1)
                .Select(offset => startOfLast7Days.AddDays(offset))
                .ToList();

            List<DateTime> daysInDay = [today.Date];

            var dayEntries = new List<DataEntry>();
            var weekEntries = new List<DataEntry>();
            var monthEntries = new List<DataEntry>();

            foreach (var date in daysInDay) {
                var inputDay = await _optionRepository.GetAll()
                    .Where(o => o.UpdateAt.Date == date)
                    .SumAsync(x => x.Quantity);

                var outputDay = await _orderRepository.GetAll()
                    .Include(o => o.Options)
                    .Where(o => o.CreateAt.Date == date)
                    .SumAsync(option => option.Quantity);

                dayEntries.Add(new DataEntry {
                    Date = date.ToString("yyyy-MM-dd"),
                    Input = inputDay,
                    Output = outputDay
                });
            }

            foreach (var date in daysInWeek) {
                var inputWeek = await _optionRepository.GetAll()
                    .Where(o => o.UpdateAt.Date == date)
                    .SumAsync(x => x.Quantity);

                var outputWeek = await _orderRepository.GetAll()
                    .Include(o => o.Options)
                    .Where(o => o.CreateAt.Date == date)
                    .SumAsync(option => option.Quantity);

                weekEntries.Add(new DataEntry {
                    Date = date.ToString("yyyy-MM-dd"),
                    Input = inputWeek,
                    Output = outputWeek
                });
            }

            foreach (var date in daysInMonth) {
                var inputMonth = await _optionRepository.GetAll()
                    .Where(o => o.UpdateAt.Date == date)
                    .SumAsync(x => x.Quantity);

                var outputMonth = await _orderRepository.GetAll()
                    .Include(o => o.Options)
                    .Where(o => o.CreateAt.Date == date)
                    .SumAsync(option => option.Quantity);

                monthEntries.Add(new DataEntry {
                    Date = date.ToString("yyyy-MM-dd"),
                    Input = inputMonth,
                    Output = outputMonth
                });
            }

            var response = new AreaChartResponse {
                Day = dayEntries,
                Week = weekEntries,
                Month = monthEntries
            };

            var result = new Response<AreaChartResponse>(HttpStatusCode.OK, response);
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));
            return result;
        }


        public async Task<Response<List<PieChartResponse>>> GetPieChartAsync() {
            var cacheKey = $"Analytics_PieChart";

            var cachedData = await _cacheService.GetData<Response<List<PieChartResponse>>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }

            var currentDate = DateTime.UtcNow;

            var lastSixMonths = Enumerable.Range(0, 6)
                .Select(i => currentDate.AddMonths(-i))
                .Select(date => new { date.Year, date.Month, MonthName = date.ToString("MMM") })
                .ToList();

            var sixMonthsAgo = currentDate.AddMonths(-6);

            var userData = await _userManager.Users
                .Where(u => u.UpdateAt >= sixMonthsAgo)
                .GroupBy(u => new { u.UpdateAt.Year, u.UpdateAt.Month })
                .Select(g => new {
                    g.Key.Year,
                    g.Key.Month,
                    Account = g.Count()
                })
                .ToListAsync();

            var resultResponse = lastSixMonths
                .GroupJoin(userData,
                    month => new { month.Year, month.Month },
                    data => new { data.Year, data.Month },
                    (month, dataGroup) => new PieChartResponse {
                        Month = month.MonthName,
                        Account = dataGroup.FirstOrDefault()?.Account ?? 0
                    })
                .OrderByDescending(r => r.Month)
                .ToList();

            var result = new Response<List<PieChartResponse>>(HttpStatusCode.OK, resultResponse);
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));
            return result;
        }



        public async Task<Response<AnalyticCountResponse>> GetCountResponseAsync() {

            var customers = await _userManager.GetUsersInRoleAsync(Role.CUSTOMER);

            var orders = await _orderRepository.GetAll()
                .Include(s => s.Status)
                .Where(o => o.Status.All(st => st.Status == EOrderStatus.SUCCESS))
                .CountAsync();

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

            var orderQuery = _orderRepository.GetAll().Include(s => s.Status).AsSplitQuery();

            if (filterStatus.HasValue) {
                orderQuery = orderQuery.Where(o => o.Status.All(st => st.Status == filterStatus.Value));
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
            var orders = await orderQuery
                    .Select(order => new {
                        Order = order,
                        LatestStatus = order.Status
                            .OrderByDescending(st => st.TimeStamp)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

            var orderResponseList = orders.Select(o => new StatisticalOrderResponse {
                Id = o.Order.Id,
                Customer = o.Order.Customer,
                Email = o.Order.Email,
                Status = o.LatestStatus!.Status,
                Address = o.Order.Address,
                NumberPhone = o.Order.NumberPhone!,
                Payment = o.Order.Payment,
                Quantity = o.Order.Quantity,
                TotalPrice = o.Order.TotalPrice,
                CreateAt = o.Order.CreateAt,
            }).ToList();

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
                   .Where(x => x.UserId == user.Id && !x.IsDeleted)
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

        public async Task<Response<AnalyticStatisticsResponse>> GetAnalyticStatisticsAsync() {
            var cacheKey = $"Analytics_Statistics";

            var cachedData = await _cacheService.GetData<Response<AnalyticStatisticsResponse>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }

            var currentDate = DateTime.UtcNow;
            var currentMonth = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var previousMonth = currentMonth.AddMonths(-1);

            var orders = await _orderRepository.GetAll().ToListAsync();
            var options = await _optionRepository.GetAll().ToListAsync();

            var ordersWithLatestStatus = await _orderRepository.GetAll()
                .Include(o => o.Status)
                .Select(o => new {
                    Order = o,
                    LatestStatus = o.Status
                        .OrderByDescending(st => st.TimeStamp)
                        .FirstOrDefault()
                })
                .ToListAsync();


            var currentMonthOrderCount = ordersWithLatestStatus.Count(o => o.LatestStatus != null
                    && o.LatestStatus.Status == EOrderStatus.SUCCESS
                    && o.Order.CreateAt >= currentMonth
                    && o.Order.CreateAt < currentMonth.AddMonths(1)
            );

            var previousMonthOrderCount = ordersWithLatestStatus.Count(o => o.LatestStatus != null
                    && o.LatestStatus.Status == EOrderStatus.SUCCESS
                    && o.Order.CreateAt >= previousMonth
                    && o.Order.CreateAt < currentMonth
            );

            var currentMonthSaleCount = ordersWithLatestStatus
                .Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS
                    && o.Order.CreateAt >= currentMonth
                    && o.Order.CreateAt < currentMonth.AddMonths(1))
                .Sum(o => o.Order.Quantity);

            var previousMonthSaleCount = ordersWithLatestStatus
                .Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS
                && o.Order.CreateAt >= previousMonth && o.Order.CreateAt < currentMonth)
                .Sum(o => o.Order.Quantity);

            var currentMonthRevenueCount = ordersWithLatestStatus
                .Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS
                && o.Order.CreateAt >= currentMonth && o.Order.CreateAt < currentMonth.AddMonths(1))
                .Sum(o => o.Order.TotalPrice);

            var previousMonthRevenueCount = ordersWithLatestStatus
                .Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS
                 && o.Order.CreateAt >= previousMonth && o.Order.CreateAt < currentMonth)
                .Sum(o => o.Order.TotalPrice);

            var currentMonthProductCount = options.Where(o => o.CreateAt >= currentMonth && o.CreateAt < currentMonth.AddMonths(1) && !o.IsDeleted)
                                                  .Sum(o => o.Quantity);
            var previousMonthProductCount = options.Where(o => o.CreateAt >= previousMonth && o.CreateAt < currentMonth && !o.IsDeleted)
                                                   .Sum(o => o.Quantity);

            static double CalculateGrowthPercentage(double current, double previous) {
                if (previous > 0) {
                    return (current - previous) / previous * 100;
                } else if (current > 0) {
                    return 100;
                }
                return 0;
            }

            var growthPercentageOrder = CalculateGrowthPercentage(currentMonthOrderCount, previousMonthOrderCount);
            var growthPercentageSale = CalculateGrowthPercentage(currentMonthSaleCount, previousMonthSaleCount);
            var growthPercentageRevenue = CalculateGrowthPercentage(currentMonthRevenueCount, previousMonthRevenueCount);
            var growthPercentageProduct = CalculateGrowthPercentage(currentMonthProductCount, previousMonthProductCount);

            var response = new AnalyticStatisticsResponse {
                Order = new Statistic {
                    IsStock = growthPercentageOrder >= 0,
                    Stock = (int)Math.Abs(growthPercentageOrder),
                    Total = currentMonthOrderCount,
                },
                SaleOut = new Statistic {
                    IsStock = growthPercentageSale >= 0,
                    Stock = (int)Math.Abs(growthPercentageSale),
                    Total = currentMonthSaleCount,
                },
                Revenue = new Statistic {
                    IsStock = growthPercentageRevenue >= 0,
                    Stock = (int)Math.Abs(growthPercentageRevenue),
                    Total = currentMonthRevenueCount,
                },
                Warehouse = new Statistic {
                    IsStock = growthPercentageProduct >= 0,
                    Stock = (int)Math.Abs(growthPercentageProduct),
                    Total = currentMonthProductCount,
                }
            };

            var result = new Response<AnalyticStatisticsResponse>(HttpStatusCode.OK, response);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));

            return result;
        }

        public async Task<Response<AnalyticTopProductResponse>> GetAnalyticTopProductsAsync() {
            var cacheKey = $"Analytics_TopProducts";

            var cachedData = await _cacheService.GetData<Response<AnalyticTopProductResponse>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }

            var currentDate = DateTime.UtcNow;

            var startOfToday = currentDate.Date.ToUniversalTime();
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date.ToUniversalTime();
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var topProductsDay = await GetTopSellingProducts(startOfToday, startOfToday.AddDays(1).ToUniversalTime());
            var topProductsWeek = await GetTopSellingProducts(startOfWeek, startOfWeek.AddDays(7).ToUniversalTime());
            var topProductsMonth = await GetTopSellingProducts(startOfMonth, startOfMonth.AddMonths(1).ToUniversalTime());

            var response = new AnalyticTopProductResponse {
                Day = topProductsDay,
                Week = topProductsWeek,
                Month = topProductsMonth
            };

            var result = new Response<AnalyticTopProductResponse>(HttpStatusCode.OK, response);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));

            return result;
        }

        public async Task<Response<AnalyticTopCustomerResponse>> GetAnalyticTopCustomerAsync() {
            var cacheKey = $"Analytics_Customers";

            var cachedData = await _cacheService.GetData<Response<AnalyticTopCustomerResponse>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }
            var currentDate = DateTime.UtcNow;

            var startOfToday = currentDate.Date.ToUniversalTime();
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date.ToUniversalTime();
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var topCustomersDay = await GetTopCustomersAsync(startOfToday, startOfToday.AddDays(1));
            var topCustomersWeek = await GetTopCustomersAsync(startOfWeek, startOfWeek.AddDays(7));
            var topCustomersMonth = await GetTopCustomersAsync(startOfMonth, startOfMonth.AddMonths(1));

            var response = new AnalyticTopCustomerResponse {
                Day = topCustomersDay,
                Week = topCustomersWeek,
                Month = topCustomersMonth
            };

            var result = new Response<AnalyticTopCustomerResponse>(HttpStatusCode.OK, response);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));

            return result;
        }

        private async Task<List<TopProduct>> GetTopSellingProducts(DateTime startDate, DateTime endDate, int topCount = 10) {
            var topProducts = await _orderRepository.GetAll()
                .Where(o => o.CreateAt >= startDate && o.CreateAt < endDate)
                .SelectMany(o => o.Options)
                .GroupBy(o => new { o.ProductId, o.Product.Name, o.Product.Brand, o.Product.Sold, o.Product.Thumbnail })
                .Select(g => new TopProduct {
                    Id = g.Key.ProductId,
                    Name = g.Key.Name,
                    Brand = g.Key.Brand!,
                    Sold = g.Key.Sold,
                    Thumbnail = g.Key.Thumbnail
                })
                .OrderByDescending(tp => tp.Sold)
                .Take(topCount)
                .ToListAsync();

            return topProducts;
        }

        private async Task<List<TopCustomer>> GetTopCustomersAsync(DateTime startDate, DateTime endDate, int topCount = 10) {
#pragma warning disable CS8602
            var topCustomers = await _orderRepository.GetAll()
                .Where(o => o.CreateAt >= startDate && o.CreateAt < endDate)
                .GroupBy(o => new { o.UserId, o.User.FirstName, o.User.LastName, o.User.Email, o.User.Avatar })
                .Select(g => new TopCustomer {
                    Id = !string.IsNullOrEmpty(g.Key.UserId) ? Guid.Parse(g.Key.UserId) : Guid.Empty,
                    CustomerName = $"{g.Key.FirstName} {g.Key.LastName}",
                    Email = g.Key.Email!,
                    Avatar = g.Key.Avatar ?? string.Empty,
                    TotalPrice = g.Sum(o => o.TotalPrice),
                    Rank = GetRank(g.Sum(o => o.TotalPrice))
                })
                .OrderByDescending(tc => tc.TotalPrice)
                .Take(topCount)
                .ToListAsync();
#pragma warning restore CS8602

            return topCustomers;
        }

        private static string GetRank(double totalPrice) {
            var rankThreshold = rank.Keys.LastOrDefault(k => k.Item1 <= totalPrice && (k.Item2 == null || k.Item2 >= totalPrice));

            if (!rankThreshold.Equals(default)) {
                return rank[rankThreshold];
            } else {
                return string.Empty;
            }
        }




    }
}