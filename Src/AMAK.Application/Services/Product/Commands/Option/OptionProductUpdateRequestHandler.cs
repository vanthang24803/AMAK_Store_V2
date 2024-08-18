using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Commands.Option {
    public class OptionProductUpdateRequestHandler : IRequestHandler<UpdateProductOptionCommand, Response<string>> {
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Option> _optionRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public OptionProductUpdateRequestHandler(IMapper mapper, ICacheService cacheService, IRepository<Domain.Models.Option> optionRepository, IRepository<Domain.Models.Product> productRepository) {
            _mapper = mapper;
            _cacheService = cacheService;
            _optionRepository = optionRepository;
            _productRepository = productRepository;
        }

        public async Task<Response<string>> Handle(UpdateProductOptionCommand request, CancellationToken cancellationToken) {
            var cacheKey = $"GetDetailProduct_{request.ProductId}";

            var existingProduct = await _productRepository.GetAll()
                                                         .Include(o => o.Options)
                                                         .Where(x => !x.IsDeleted)
                                                         .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
           ?? throw new NotFoundException("Product not found!");

            try {
                await _optionRepository.BeginTransactionAsync();

                foreach (var option in request.Data.Options) {
                    if (option.Id == null) {
                        var newOption = _mapper.Map<Domain.Models.Option>(option);
                        newOption.Id = Guid.NewGuid();
                        newOption.ProductId = existingProduct.Id;
                        _optionRepository.Add(newOption);
                    } else {
                        var existOption = await _optionRepository.GetAll()
                                                                 .Where(x => x.Id == option.Id && !x.IsDeleted)
                                                                 .FirstOrDefaultAsync(cancellationToken)
                                        ?? throw new NotFoundException("Option not found");

                        if (option.IsDelete != null) {
                            _mapper.Map(option, existOption);
                            _optionRepository.Update(existOption);
                        } else {
                            existOption.IsDeleted = true;
                            _optionRepository.Update(existOption);
                        }
                    }
                }

                await _optionRepository.SaveChangesAsync();

                await _optionRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _optionRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query Error!");
            }


            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(System.Net.HttpStatusCode.OK, "Update option successfully!");
        }
    }
}