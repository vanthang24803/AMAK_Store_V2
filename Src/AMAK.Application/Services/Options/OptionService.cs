using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Options.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Options {
    public class OptionService : IOptionsService {

        private readonly IRepository<Domain.Models.Option> _optionRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly ICacheService _cacheService;

        private readonly IMapper _mapper;

        public OptionService(IRepository<Domain.Models.Option> optionRepository, IRepository<Domain.Models.Product> productRepository, IMapper mapper, ICacheService cacheService) {
            _productRepository = productRepository;
            _optionRepository = optionRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<Response<OptionResponse>> CreateAsync(Guid productId, OptionRequest request) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingProduct = await _productRepository.GetById(productId) ?? throw new NotFoundException("Product not found!");

            var newOption = _mapper.Map<Domain.Models.Option>(request);

            newOption.Id = Guid.NewGuid();
            newOption.ProductId = existingProduct.Id;

            _optionRepository.Add(newOption);

            await _optionRepository.SaveChangesAsync();

            await _cacheService.RemoveData(cacheKey);

            return new Response<OptionResponse>(HttpStatusCode.Created, _mapper.Map<OptionResponse>(newOption));
        }

        public async Task<Response<string>> DeleteAsync(Guid productId, Guid id) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingOption = await _optionRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id && x.ProductId == productId && !x.IsDeleted) ?? throw new NotFoundException("Option not found!");

            if (existingOption.IsDeleted) {
                _optionRepository.Remove(existingOption);
            } else {
                existingOption.IsDeleted = true;
                _optionRepository.Update(existingOption);
            }

            await _optionRepository.SaveChangesAsync();

            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(HttpStatusCode.OK, "Option deleted successfully!");
        }

        public async Task<Response<List<OptionResponse>>> GetAllAsync(Guid productId) {
            var options = await _optionRepository.GetAll().Where(x => !x.IsDeleted).ToListAsync();

            return new Response<List<OptionResponse>>(HttpStatusCode.OK, _mapper.Map<List<OptionResponse>>(options));
        }

        public async Task<Response<OptionResponse>> GetAsync(Guid productId, Guid id) {
            var existingOption = await _optionRepository.GetAll()
                   .FirstOrDefaultAsync(x => x.Id == id && x.ProductId == productId && !x.IsDeleted) ?? throw new NotFoundException("Option not found!");

            return new Response<OptionResponse>(HttpStatusCode.OK, _mapper.Map<OptionResponse>(existingOption));
        }

        public async Task<Response<OptionResponse>> UpdateAsync(Guid id, Guid productId, OptionRequest request) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingOption = await _optionRepository.GetAll()
                 .FirstOrDefaultAsync(x => x.Id == id && x.ProductId == productId && !x.IsDeleted) ?? throw new NotFoundException("Option not found!");

            _mapper.Map(existingOption, request);

            await _productRepository.SaveChangesAsync();

            await _cacheService.RemoveData(cacheKey);

            return new Response<OptionResponse>(HttpStatusCode.OK, _mapper.Map<OptionResponse>(existingOption));

        }
    }
}