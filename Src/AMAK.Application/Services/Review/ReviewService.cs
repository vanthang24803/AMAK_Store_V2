using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Review.Dtos;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.AspNetCore.Http;
using AMAK.Application.Providers.Upload;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Review {
    public class ReviewService : IReviewService {

        private readonly IRepository<Domain.Models.Review> _reviewRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Domain.Models.ReviewPhoto> _reviewPhotoRepository;

        private readonly IUploadService _uploadService;


        public ReviewService(IRepository<Domain.Models.Review> reviewRepository, IRepository<Domain.Models.Product> productRepository, UserManager<ApplicationUser> userManager, IRepository<ReviewPhoto> reviewPhotoRepository, IUploadService uploadService) {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _reviewPhotoRepository = reviewPhotoRepository;
            _uploadService = uploadService;
        }

        public async Task<Response<string>> CreateAsync(ClaimsPrincipal claims, CreateReviewRequest request, List<IFormFile> files) {
            var existingAccount = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");


            var existingProduct = await _productRepository.GetById(request.ProductId) ?? throw new NotFoundException("Product not found!");

            var newReview = new Domain.Models.Review() {
                Id = Guid.NewGuid(),
                Star = request.Star,
                Content = request.Content,
                ProductId = existingProduct.Id,
                UserId = existingAccount.Id
            };

            _reviewRepository.Add(newReview);


            await _reviewRepository.SaveChangesAsync();


            foreach (var file in files) {
                var upload = await _uploadService.UploadPhotoAsync(file);

                if (upload.Error != null) {
                    throw new BadRequestException(message: upload.Error.Message);
                }

                var newPhoto = new Domain.Models.ReviewPhoto() {
                    Id = Guid.NewGuid(),
                    Url = upload.SecureUrl.AbsoluteUri,
                    PublicId = upload.PublicId,
                    ReviewId = newReview.Id
                };

                _reviewPhotoRepository.Add(newPhoto);
            }

            await _reviewPhotoRepository.SaveChangesAsync();


            return new Response<string>(HttpStatusCode.Created, "Review created!");
        }

        public async Task<List<Domain.Models.Review>> GetAllAsync(Guid productId) {
                 var reviews = await _reviewRepository.GetAll().Where(p => p.ProductId == productId).ToListAsync();

            return reviews;
        }

        public Task<Response<Domain.Models.Review>> GetAsync(ClaimsPrincipal claims, Guid productId, Guid id) {
            throw new NotImplementedException();
        }


        // public async Task<PaginationResponse<ReviewResponse>> GetAllAsync(ClaimsPrincipal claims, Guid productId) {
        //     var reviews = await _reviewRepository.GetAll().Where(p => p.ProductId == productId).ToListAsync();

        //     throw new NotImplementedException();
        // }

        // public async Task<Response<ReviewResponse>> GetAsync(ClaimsPrincipal claims, Guid productId, Guid id) {
        //     var existingReview = await _reviewRepository.GetAll().Include(p => p.Photos).FirstOrDefaultAsync(
        //          p => p.ProductId == productId && p.Id == id
        //     ) ?? throw new NotFoundException("Review not found!");

        //     return new Response<ReviewResponse>(HttpStatusCode.OK, _mapper.Map<ReviewResponse>(existingReview));
        // }




    }
}