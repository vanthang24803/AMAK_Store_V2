using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Product.Commands.Import {
    public class ImportExcelToProductCommandHandler : IRequestHandler<ImportExcelToProductCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly IRepository<Domain.Models.ProductCategory> _productCategoryRepository;

        private readonly IRepository<Domain.Models.Option> _optionRepository;

        private readonly IRepository<Domain.Models.Photo> _photoRepository;


        public ImportExcelToProductCommandHandler(IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.ProductCategory> productCategoryRepository, IRepository<Domain.Models.Option> optionRepository, IRepository<Domain.Models.Photo> photoRepository) {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _optionRepository = optionRepository;
            _photoRepository = photoRepository;
        }


        public async Task<Response<string>> Handle(ImportExcelToProductCommand request, CancellationToken cancellationToken) {
            if (ValidationFile(request.File)) {
                await ImportDataFromExcel(request.File);
                return new Response<string>(System.Net.HttpStatusCode.Created, "Data imported successfully!");
            }

            throw new NotImplementedException();
        }

        private async Task ImportDataFromExcel(IFormFile file) {
            using var stream = file.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
            var productsSheet = workbook.Worksheet("Products");
            var categoriesSheet = workbook.Worksheet("Categories");
            var optionsSheet = workbook.Worksheet("Options");
            var photosSheet = workbook.Worksheet("Photos");

            try {
                await _productRepository.BeginTransactionAsync();

                var products = productsSheet.RowsUsed().Skip(1).Select(row => new Domain.Models.Product {
                    Id = Guid.Parse(row.Cell(1).GetValue<string>()),
                    Name = row.Cell(2).GetValue<string>(),
                    Brand = row.Cell(3).GetValue<string>(),
                    Thumbnail = row.Cell(4).GetValue<string>(),
                }).ToList();

                _productRepository.AddRange(products);

                await _photoRepository.SaveChangesAsync();


                var productCategories = categoriesSheet.RowsUsed().Skip(1).Select(row => new Domain.Models.ProductCategory {
                    ProductId = Guid.Parse(row.Cell(1).GetValue<string>()),
                    CategoryId = Guid.Parse(row.Cell(2).GetValue<string>())
                }).ToList();

                _productCategoryRepository.AddRange(productCategories);

                await _productCategoryRepository.SaveChangesAsync();


                var options = optionsSheet.RowsUsed().Skip(1).Select(row => new Domain.Models.Option {
                    Id = Guid.NewGuid(),
                    Name = row.Cell(1).GetValue<string>(),
                    Sale = row.Cell(2).GetValue<int>(),
                    Quantity = row.Cell(3).GetValue<int>(),
                    Price = row.Cell(4).GetValue<long>(),
                    ProductId = Guid.Parse(row.Cell(5).GetValue<string>()),
                    IsActive = true
                });

                _optionRepository.AddRange(options);

                await _optionRepository.SaveChangesAsync();


                var photos = photosSheet.RowsUsed().Skip(1).Select(row => new Domain.Models.Photo {
                    Id = Guid.NewGuid(),
                    Url = row.Cell(1).GetValue<string>(),
                    ProductId = Guid.Parse(row.Cell(2).GetValue<string>()),
                });

                _photoRepository.AddRange(photos);

                await _photoRepository.SaveChangesAsync();

                await _productRepository.CommitTransactionAsync();

            } catch (Exception) {
                await _productRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query wrong!");
            }

        }
        private static bool ValidationFile(IFormFile file) {
            if (file.Length <= 0) {
                throw new BadRequestException("File is required!");
            }

            List<string> validExtensions = [".xlsx"];

            string extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension)) {
                throw new BadRequestException("File type is not excel file!");
            }

            long size = file.Length;

            if (size > 5 * 1024 * 1024) {
                throw new BadRequestException("Max file size can be 5mb!");
            }

            return true;
        }
    }
}