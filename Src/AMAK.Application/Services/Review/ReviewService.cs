using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Review.Dtos;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.AspNetCore.Http;
using AMAK.Application.Providers.Cloudinary;
using Microsoft.EntityFrameworkCore;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Photo.Dtos;
using AutoMapper;
using AMAK.Application.Providers.Cache;

namespace AMAK.Application.Services.Review {
    public class ReviewService : IReviewService {
        private readonly IRepository<Domain.Models.Review> _reviewRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<ReviewPhoto> _reviewPhotoRepository;
        private readonly ICloudinaryService _CloudinaryService;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public ReviewService(
            IRepository<Domain.Models.Review> reviewRepository,
            IRepository<Domain.Models.Product> productRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<ReviewPhoto> reviewPhotoRepository,
            ICloudinaryService CloudinaryService,
            IRepository<Domain.Models.Order> orderRepository,
            IRepository<OrderDetail> orderDetailRepository,
            IMapper mapper,
            ICacheService cacheService
        ) {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _reviewPhotoRepository = reviewPhotoRepository;
            _CloudinaryService = CloudinaryService;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<Response<string>> CreateAsync(ClaimsPrincipal claims, CreateReviewRequest request, List<IFormFile> files) {
            var existingAccount = await _userManager.GetUserAsync(claims) ?? throw new NotFoundException("Account not found!");

            var existingOrder = await _orderRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.OrderId && !x.IsDeleted && !x.IsReviewed && x.UserId == existingAccount.Id)
                ?? throw new NotFoundException("Order not found!");

            var listProductIds = await _orderDetailRepository.GetAll()
                .Where(x => x.OrderId == existingOrder.Id)
                .Select(x => x.ProductId)
                .ToListAsync();

            var existingProducts = await _productRepository.GetAll()
                .Where(p => listProductIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var reviews = new List<Domain.Models.Review>();
            var reviewPhotos = new List<ReviewPhoto>();

            await _reviewRepository.BeginTransactionAsync();

            try {
                foreach (var productId in listProductIds) {
                    if (!existingProducts.TryGetValue(productId, out var existingProduct)) {
                        throw new NotFoundException("Product not found!");
                    }

                    var newReview = new Domain.Models.Review {
                        Id = Guid.NewGuid(),
                        Star = request.Star,
                        Content = request.Content,
                        ProductId = existingProduct.Id,
                        UserId = existingAccount.Id
                    };

                    reviews.Add(newReview);

                    foreach (var file in files) {
                        var upload = await _CloudinaryService.UploadPhotoAsync(file);
                        if (upload.Error != null) {
                            throw new BadRequestException(upload.Error.Message);
                        }

                        var newPhoto = new ReviewPhoto {
                            Id = Guid.NewGuid(),
                            Url = upload.SecureUrl.AbsoluteUri,
                            PublicId = upload.PublicId,
                            ReviewId = newReview.Id
                        };

                        reviewPhotos.Add(newPhoto);
                    }
                }

                _reviewRepository.AddRange(reviews);
                _reviewPhotoRepository.AddRange(reviewPhotos);
                await _reviewRepository.SaveChangesAsync();
                await _reviewPhotoRepository.SaveChangesAsync();

                existingOrder.IsReviewed = true;
                await _orderRepository.SaveChangesAsync();

                await _reviewRepository.CommitTransactionAsync();
            } catch (Exception ex) {
                await _reviewRepository.RollbackTransactionAsync();
                throw new BadRequestException("Failed to create review: " + ex.Message);
            }

            return new Response<string>(HttpStatusCode.Created, "Review created!");
        }

        public async Task<ListReviewResponse<List<ReviewResponse>>> GetAllAsync(Guid productId, ReviewQuery query) {
            var cacheKey = $"Review_Product_{productId}_{query.Limit}_{query.Page}_{query.Status}_{query.Star}";

            var cachedData = await _cacheService.GetData<ListReviewResponse<List<ReviewResponse>>>(cacheKey);
            if (cachedData != null) return cachedData;

            var existingProduct = await _productRepository.GetById(productId) ?? throw new NotFoundException("Product not found!");

            var allReviews = await _reviewRepository.GetAll()
                .Include(x => x.Photos)
                .Include(x => x.User)
                .Where(x => !x.IsDeleted && x.ProductId == existingProduct.Id)
                .ToListAsync();

            var result = Paginate(allReviews, query);
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(5));

            return result;
        }

        public async Task<ListReviewResponse<List<ReviewResponse>>> GetAsync(ClaimsPrincipal claims, ReviewQuery query) {
            var existingAccount = await _userManager.GetUserAsync(claims) ?? throw new NotFoundException("Account not found!");

            var cacheKey = $"Review_Account_{existingAccount.Id}_{query.Limit}_{query.Page}_{query.Status}_{query.Star}";

            var cachedData = await _cacheService.GetData<ListReviewResponse<List<ReviewResponse>>>(cacheKey);
            if (cachedData != null) return cachedData;

            var allReviews = await _reviewRepository.GetAll()
                .Include(x => x.Photos)
                .Include(x => x.User)
                .Where(x => !x.IsDeleted && x.UserId == existingAccount.Id)
                .ToListAsync();

            var result = Paginate(allReviews, query);
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(5));

            return result;
        }

        public async Task<Response<ReviewResponse>> GetOneAsync(Guid id) {
            var cacheKey = $"Review_Id_{id}";

            var cachedData = await _cacheService.GetData<Response<ReviewResponse>>(cacheKey);

            var existingReview = await _reviewRepository.GetAll()
                                                        .Include(x => x.Photos)
                                                        .Include(x => x.User)
                                                        .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id) ?? throw new NotFoundException("Review not found!");

            var result = new ReviewResponse {
                Id = existingReview.Id,
                Star = existingReview.Star,
                Content = existingReview.Content,
                Author = _mapper.Map<ProfileReviewResponse>(existingReview.User),
                Photos = existingReview.Photos.Select(photo => new PhotoResponse(
                    photo.Id,
                    photo.Url,
                    photo.CreateAt
                )).ToList(),
                CreateAt = existingReview.CreateAt,
            };

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(5));

            return new Response<ReviewResponse>(HttpStatusCode.OK, result);
        }

        public async Task<Response<string>> RemoveAsync(Guid id) {
            var existingReview = await _reviewRepository.GetById(id) ?? throw new NotFoundException("Review not found!");

            existingReview.IsDeleted = true;

            await _reviewPhotoRepository.SaveChangesAsync();

            await _cacheService.RemoveData($"Review_Id_{existingReview.Id}");

            return new Response<string>(HttpStatusCode.OK, "Review hidden successfully!");
        }

        private ListReviewResponse<List<ReviewResponse>> Paginate(List<Domain.Models.Review> allReviews, ReviewQuery query) {

            if (!string.IsNullOrEmpty(query.Status)) {
                if (query.Status == "Lasted") {

                    allReviews = [.. allReviews.OrderByDescending(r => r.CreateAt)];
                }

                if (query.Status == "Image") {
                    allReviews = allReviews.Where(n => n.Photos.Count != 0).ToList();
                }
            }

            if (!string.IsNullOrEmpty(query.Star) && int.TryParse(query.Star, out int starValue)) {
                allReviews = allReviews.Where(n => n.Star == starValue).ToList();
            }

            var totalItems = allReviews.Count;
            var totalPage = (int)Math.Ceiling(totalItems / (double)query.Limit);
            var skip = (query.Page - 1) * query.Limit;

            var paginatedReviews = allReviews
                .Skip(skip)
                .Take(query.Limit)
                .Select(review => new ReviewResponse {
                    Id = review.Id,
                    Star = review.Star,
                    Content = review.Content,
                    Author = _mapper.Map<ProfileReviewResponse>(review.User),
                    Photos = review.Photos.Select(photo => new PhotoResponse(
                        photo.Id,
                        photo.Url,
                        photo.CreateAt
                    )).ToList(),
                    CreateAt = review.CreateAt
                })
                .ToList();

            float averageStar = 0;

            if (allReviews.Count > 0) {
                averageStar = allReviews.Average(r => r.Star);
            }


            return new ListReviewResponse<List<ReviewResponse>> {
                AverageStar = averageStar,
                CurrentPage = query.Page,
                TotalPage = totalPage,
                Items = paginatedReviews.Count,
                TotalItems = totalItems,
                Result = paginatedReviews
            };
        }
    }
}
