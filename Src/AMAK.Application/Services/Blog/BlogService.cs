using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Blog.Dto;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using AMAK.Application.Providers.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Blog {
    public class BlogService : IBlogService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Domain.Models.Blog> _blogRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public BlogService(IRepository<Domain.Models.Blog> blogRepository, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService) {
            _userManager = userManager;
            _blogRepository = blogRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Response<string>> CreateAsync(ClaimsPrincipal claims, BlogRequest request, IFormFile file) {
            var author = await _userManager.GetUserAsync(claims)
              ?? throw new NotFoundException("Account not found!");

            var upload = await _cloudinaryService.UploadPhotoAsync(file);

            if (upload.Error != null) {
                throw new BadRequestException(message: upload.Error.Message);
            }

            var newBlog = new Domain.Models.Blog() {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Thumbnail = upload.SecureUrl.AbsoluteUri,
                AuthorId = author.Id,
            };

            _blogRepository.Add(newBlog);

            await _blogRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.Created, "Blog Created Successfully!");
        }

        public async Task<Response<string>> DeleteAsync(ClaimsPrincipal claims, Guid blogId) {
            var author = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");

            var existingBlog = await _blogRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == blogId)
                ?? throw new NotFoundException("Blog not found!");

            if (!(author.Id == existingBlog.AuthorId)) throw new ForbiddenException();

            _blogRepository.Remove(existingBlog);

            await _blogRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Blog Deleted Successfully!");
        }

        public async Task<Response<BlogResponse>> FindOneByIdAsync(Guid id) {
            var result = await _blogRepository.GetAll()
                .Include(a => a.Author)
                .Select(b => new BlogResponse() {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    CreateAt = b.CreateAt,
                    Author = new Author() {
                        Id = b.Author.Id,
                        FullName = $"{b.Author.FirstName} {b.Author.LastName}",
                        Avatar = b.Author.Avatar ?? "",
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException("Blog not found!");

            return new Response<BlogResponse>(HttpStatusCode.OK, result);
        }

        public async Task<Response<string>> UpdateAsync(ClaimsPrincipal claims, Guid blogId, BlogRequest request, IFormFile? file) {
            var author = await _userManager.GetUserAsync(claims)
            ?? throw new NotFoundException("Account not found!");

            var existingBlog = await _blogRepository.GetAll()
             .FirstOrDefaultAsync(x => x.Id == blogId)
             ?? throw new NotFoundException("Blog not found!");

            if (!(author.Id == existingBlog.AuthorId)) throw new ForbiddenException();

            if (file != null) {
                var upload = await _cloudinaryService.UploadPhotoAsync(file);

                if (upload.Error != null) {
                    throw new BadRequestException(message: upload.Error.Message);
                }

                existingBlog.Thumbnail = upload.SecureUrl.AbsoluteUri;
            }

            existingBlog.Title = request.Title;
            existingBlog.Content = request.Content;
            existingBlog.UpdateAt = DateTime.UtcNow;

            _blogRepository.Update(existingBlog);

            await _blogRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Blog Updated Successfully!");
        }

        public async Task<PaginationResponse<List<BlogResponse>>> GetAllBlogAsync(BaseQuery query) {
            var totalItems = await _blogRepository.GetAll().CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / query.Limit);

            var blogs = await _blogRepository.GetAll()
                .Include(b => b.Author)
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .Select(b => new BlogResponse {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    CreateAt = b.CreateAt,
                    Author = new Author {
                        Id = b.Author.Id,
                        FullName = $"{b.Author.FirstName} {b.Author.LastName}",
                        Avatar = b.Author.Avatar ?? "",
                    }
                })
                .ToListAsync();

            return new PaginationResponse<List<BlogResponse>> {
                Code = 200,
                Result = blogs,
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalItems
            };
        }


        public async Task<PaginationResponse<List<BlogResponse>>> GetAllBlogForAccountAsync(ClaimsPrincipal claims, BaseQuery query) {
            var author = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");

            var totalItems = await _blogRepository.GetAll().CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / query.Limit);

            var blogs = await _blogRepository.GetAll()
                .Include(b => b.Author)
                .Where(b => b.AuthorId == author.Id)
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .Select(b => new BlogResponse {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    CreateAt = b.CreateAt,
                    Author = new Author {
                        Id = b.Author.Id,
                        FullName = $"{b.Author.FirstName} {b.Author.LastName}",
                        Avatar = b.Author.Avatar ?? "",
                    }
                })
                .ToListAsync();

            return new PaginationResponse<List<BlogResponse>> {
                Code = 200,
                Result = blogs,
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalItems
            };
        }


    }
}