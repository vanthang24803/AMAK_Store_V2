using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Revert.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Revert {
    public class RevertService : IRevertService {
        private readonly IRepository<Domain.Models.Option> _optionRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Photo> _photoRepository;

        public RevertService(IRepository<Domain.Models.Photo> photoRepository, IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.Option> optionRepository) {
            _photoRepository = photoRepository;
            _productRepository = productRepository;
            _optionRepository = optionRepository;
        }

        public async Task<Response<string>> RevertMediaAsync(MediaRevertRequest request) {
            try {
                await _photoRepository.BeginTransactionAsync();
                foreach (var photo in request.Reverts) {
                    var existingPhoto = await _photoRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.Id == photo.Id)
                        ?? throw new NotFoundException("Photo not found!");

                    existingPhoto.IsDeleted = false;
                    existingPhoto.UpdateAt = DateTime.Now;
                    _photoRepository.Update(existingPhoto);
                }

                await _photoRepository.SaveChangesAsync();

                await _photoRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _photoRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query failed !");
            }


            return new Response<string>(HttpStatusCode.OK, "Revert successfully!");
        }

        public async Task<Response<string>> RevertOptionAsync(OptionRevertRequest request) {
            try {
                await _optionRepository.BeginTransactionAsync();
                foreach (var item in request.Reverts) {
                    var existingItem = await _optionRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.Id == item.Id)
                        ?? throw new NotFoundException("Option not found!");

                    existingItem.IsDeleted = false;
                    existingItem.UpdateAt = DateTime.Now;
                    _optionRepository.Update(existingItem);
                }

                await _optionRepository.SaveChangesAsync();

                await _optionRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _optionRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query failed !");
            }

            return new Response<string>(HttpStatusCode.OK, "Revert successfully!");
        }

        public async Task<Response<string>> RevertProductAsync(ProductRevertRequest request) {
            try {
                await _productRepository.BeginTransactionAsync();
                foreach (var item in request.Reverts) {
                    var existingItem = await _productRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.Id == item.Id)
                        ?? throw new NotFoundException("Product not found!");

                    existingItem.IsDeleted = false;
                    existingItem.UpdateAt = DateTime.Now;
                    _productRepository.Update(existingItem);
                }

                await _productRepository.SaveChangesAsync();

                await _productRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _productRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query failed !");
            }

            return new Response<string>(HttpStatusCode.OK, "Revert successfully!");
        }
    }
}