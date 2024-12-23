using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.FlashSale.Dtos;
using AMAK.Application.Services.Product.Common;
using AMAK.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AMAK.Application.Services.FlashSale {
    public class FlashSaleService : IFlashSaleService {
        private readonly ILogger<FlashSaleService> _logger;
        private readonly IRepository<Domain.Models.FlashSale> _flashSaleRepository;
        private readonly IRepository<Option> _optionRepository;
        private readonly IRepository<FlashSaleProduct> _flashSaleProductRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IMapper _mapper;

        public FlashSaleService(IRepository<Domain.Models.FlashSale> flashSaleRepository, ILogger<FlashSaleService> logger, IRepository<FlashSaleProduct> flashSaleProductRepository, IRepository<Option> optionRepository, IMapper mapper, IRepository<Domain.Models.Product> productRepository) {
            _flashSaleRepository = flashSaleRepository;
            _logger = logger;
            _flashSaleProductRepository = flashSaleProductRepository;
            _optionRepository = optionRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response<string>> CreateAsync(CreateFlashSaleRequest request) {
            try {
                var newFlashSale = new Domain.Models.FlashSale() {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    StartAt = request.StartAt,
                    EndAt = request.EndAt,
                    Status = request.Status,
                };

                _flashSaleRepository.Add(newFlashSale);

                await _flashSaleRepository.SaveChangesAsync();


                foreach (var sale in request.Sales) {
                    var existingOption = await _optionRepository.GetAll()
                            .FirstOrDefaultAsync(x => x.ProductId == sale.ProductId && x.Id == sale.OptionId)
                            ?? throw new NotFoundException("Option not found!");

                    var newFlashSaleProduct = new FlashSaleProduct() {
                        FlashSaleId = newFlashSale.Id,
                        ProductId = sale.ProductId,
                        OptionId = sale.OptionId,
                    };

                    _flashSaleProductRepository.Add(newFlashSaleProduct);

                    await _flashSaleProductRepository.SaveChangesAsync();

                    await _optionRepository.SaveChangesAsync();
                }

                return new Response<string>(HttpStatusCode.Created, "Create Flash Sale Successfully!");


            } catch (Exception e) {
                _logger.LogError("{}", e.Message);
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<Response<List<ListFlashSaleResponse>>> FindAll() {
            var flashSale = await _flashSaleRepository.GetAll()
              .Include(x => x.Options)
              .OrderByDescending(x => x.StartAt)
              .ToListAsync();

            var response = flashSale.Select(sale => new ListFlashSaleResponse() {
                Id = sale.Id,
                Name = sale.Name,
                StartAt = sale.StartAt,
                EndAt = sale.EndAt,
                Status = sale.Status,
                Products = sale.Options.Count,
            }).ToList();


            return new Response<List<ListFlashSaleResponse>>(HttpStatusCode.OK, response);
        }

        public async Task<Response<FlashSaleResponse>> FindOne(Guid id) {
            var flashSale = await _flashSaleRepository.GetAll()
                .Include(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException("Flash Sale not found!");

            var productIds = flashSale.Options
                .Where(option => option.IsFlashSale)
                .Select(option => option.ProductId)
                .ToList();

            var products = await _productRepository.GetAll()
                .Where(p => productIds.Contains(p.Id))
                .Include(c => c.Categories)
                .Include(p => p.Photos)
                .Include(x => x.Options)
                .ToListAsync();

            foreach (var product in products) {
                product.Options = product.Options
                    .Where(x => x.IsFlashSale)
                    .ToList();
            }

            var response = new FlashSaleResponse() {
                Id = flashSale.Id,
                StartAt = flashSale.StartAt,
                EndAt = flashSale.EndAt,
                Status = flashSale.Status,
                Products = _mapper.Map<List<ProductResponse>>(products)
            };

            return new Response<FlashSaleResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<FlashSaleResponse>> GetFlashSaleAsync() {
            var flashSale = await _flashSaleRepository.GetAll()
                .Include(x => x.Options)
                .FirstOrDefaultAsync(x => x.Status == Domain.Enums.EFlashSale.ACTIVE)
                ?? throw new NotFoundException("No sales campaigns!");

            var productIds = flashSale.Options
                .Where(option => option.IsFlashSale)
                .Select(option => option.ProductId)
                .ToList();

            var products = await _productRepository.GetAll()
                .Where(p => productIds.Contains(p.Id))
                .Include(c => c.Categories)
                .Include(p => p.Photos)
                .Include(x => x.Options)
                .ToListAsync();

            foreach (var product in products) {
                product.Options = product.Options
                    .Where(x => x.IsFlashSale)
                    .ToList();
            }

            var response = new FlashSaleResponse() {
                Id = flashSale.Id,
                Name = flashSale.Name,
                StartAt = flashSale.StartAt,
                EndAt = flashSale.EndAt,
                Status = flashSale.Status,
                Products = _mapper.Map<List<ProductResponse>>(products)
            };

            return new Response<FlashSaleResponse>(HttpStatusCode.OK, response);
        }

        public async Task JobFlashSale() {
            await _flashSaleRepository.BeginTransactionAsync();

            try {
                var flashSale = await _flashSaleRepository.GetAll()
                    .Include(x => x.Options)
                    .FirstOrDefaultAsync(x =>
                        (x.Status == Domain.Enums.EFlashSale.PENDING && x.StartAt <= DateTime.UtcNow) ||
                        (x.Status == Domain.Enums.EFlashSale.ACTIVE && x.EndAt <= DateTime.UtcNow)
                    );

                if (flashSale == null) {
                    return;
                }

                if (flashSale.Status == Domain.Enums.EFlashSale.ACTIVE && flashSale.EndAt <= DateTime.UtcNow) {
                    flashSale.Status = Domain.Enums.EFlashSale.COMPLETED;

                    foreach (var option in flashSale.Options) {
                        option.IsFlashSale = false;
                    }
                } else if (flashSale.Status == Domain.Enums.EFlashSale.PENDING && flashSale.StartAt <= DateTime.UtcNow) {
                    flashSale.Status = Domain.Enums.EFlashSale.ACTIVE;

                    foreach (var option in flashSale.Options) {
                        option.IsFlashSale = true;
                    }
                }

                await _optionRepository.SaveChangesAsync();
                await _flashSaleRepository.SaveChangesAsync();

                await _flashSaleProductRepository.CommitTransactionAsync();
            } catch (Exception ex) {
                await _flashSaleProductRepository.RollbackTransactionAsync();
                _logger.LogError("{}", ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }
    }
}