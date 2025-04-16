using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Trash.Dtos;
using AMAK.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Trash {
    public class TrashService : ITrashService {
        private readonly IRepository<Option> _optionRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Photo> _photoRepository;
        public TrashService(IRepository<Domain.Models.Product> productRepository, IRepository<Option> optionRepository, IRepository<Domain.Models.Photo> photoRepository) {
            _productRepository = productRepository;
            _optionRepository = optionRepository;
            _photoRepository = photoRepository;
        }

        public async Task<Response<List<OptionDeletedResponse>>> GetOptionTrashAsync() {
            var options = await _optionRepository.GetAll()
                .Where(o => o.IsDeleted)
                .Select(c => new OptionDeletedResponse() {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    Sale = c.Sale,
                    Quantity = c.Quantity,
                    DeletedAt = c.DeleteAt
                })
                .ToListAsync();

            return new Response<List<OptionDeletedResponse>>(HttpStatusCode.OK, options);
        }

        public async Task<Response<List<PhotoDeletedResponse>>> GetPhotoResponse() {
            var media = await _photoRepository.GetAll()
                .Where(o => o.IsDeleted)
                .Select(c => new PhotoDeletedResponse() {
                    Id = c.Id,
                    Url = c.Url,
                    DeletedAt = c.DeleteAt
                }).ToListAsync();

            return new Response<List<PhotoDeletedResponse>>(HttpStatusCode.OK, media);
        }

        public async Task<Response<List<ProductDeletedResponse>>> GetProductTrashAsync() {
            var products = await _productRepository.GetAll()
            .Include(o => o.Options)
            .Where(o => o.IsDeleted)
            .Select(p => new ProductDeletedResponse() {
                Id = p.Id,
                Name = p.Name,
                Thumbnail = p.Thumbnail ?? string.Empty,
                Brand = p.Brand ?? string.Empty,
                Options = p.Options.Count(),
                Sold = p.Sold,
                DeletedAt = p.DeleteAt
            }).ToListAsync();

            return new Response<List<ProductDeletedResponse>>(HttpStatusCode.OK, products);
        }
    }
}