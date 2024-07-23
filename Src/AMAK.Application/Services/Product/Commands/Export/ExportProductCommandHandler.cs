using System.Data;
using AMAK.Application.Interfaces;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Commands.Export {
    public class ExportProductCommandHandler : IRequestHandler<ExportProductCommand, byte[]> {

        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Category> _categoryRepository;
        private readonly IRepository<Domain.Models.Option> _optionRepository;
        private readonly IRepository<Domain.Models.Photo> _photoRepository;

        public ExportProductCommandHandler(IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.Category> categoryRepository, IRepository<Domain.Models.Option> optionRepository, IRepository<Domain.Models.Photo> photoRepository) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _optionRepository = optionRepository;
            _photoRepository = photoRepository;
        }

        public async Task<byte[]> Handle(ExportProductCommand request, CancellationToken cancellationToken) {
            var _productData = await GetProductData();

            var _categoryData = await GetCategoryData();

            var _optionData = await GetOptionData();

            var _imageData = await GetPhotoData();

            using XLWorkbook wb = new();

            var sheet1 = wb.AddWorksheet(_productData, "Products");

            var sheet2 = wb.AddWorksheet(_categoryData, "Categories");

            var sheet3 = wb.AddWorksheet(_optionData, "Options");

            var sheet4 = wb.AddWorksheet(_imageData, "Photos");


            sheet1.Column(1).Width = 40;
            sheet1.Column(2).Width = 30;
            sheet1.Column(3).Width = 50;
            sheet1.Column(4).Width = 10;
            sheet1.Column(5).Width = 25;

            sheet2.Column(1).Width = 40;
            sheet2.Column(2).Width = 25;
            sheet2.Column(3).Width = 25;

            sheet3.Column(1).Width = 40;
            sheet3.Column(2).Width = 30;
            sheet3.Column(3).Width = 20;
            sheet3.Column(4).Width = 20;
            sheet3.Column(5).Width = 20;
            sheet3.Column(6).Width = 20;
            sheet3.Column(7).Width = 60;
            sheet3.Column(8).Width = 25;

            sheet4.Column(1).Width = 40;
            sheet4.Column(2).Width = 50;
            sheet4.Column(3).Width = 50;
            sheet4.Column(4).Width = 25;

            using MemoryStream ms = new();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        private async Task<DataTable> GetProductData() {
            DataTable dt = new() {
                TableName = "Products"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Thumbnail", typeof(string));
            dt.Columns.Add("Brand", typeof(string));
            dt.Columns.Add("Delete", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = await _productRepository.GetAll().ToListAsync();

            if (_list.Count > 0) {
                _list.ForEach(item => {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Thumbnail,
                        item.Brand,
                        item.IsDeleted,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }
            return dt;
        }

        private async Task<DataTable> GetCategoryData() {
            DataTable dt = new() {
                TableName = "Categories"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));
            dt.Columns.Add("UpdateAt", typeof(string));

            var _list = await _categoryRepository.GetAll().ToListAsync();

            if (_list.Count > 0) {
                _list.ForEach(item => {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm"),
                        item.UpdateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }

        private async Task<DataTable> GetOptionData() {
            DataTable dt = new() {
                TableName = "Options"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Sale", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Active", typeof(bool));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));
            dt.Columns.Add("Delete", typeof(bool));

            var _list = await _optionRepository.GetAll().ToListAsync();

            if (_list.Count > 0) {
                _list.ForEach(item => {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Sale,
                        item.Quantity,
                        item.Price,
                        item.IsActive,
                        item.ProductId,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm"),
                        item.IsDeleted
                    );
                });
            }

            return dt;
        }

        private async Task<DataTable> GetPhotoData() {
            DataTable dt = new() {
                TableName = "Photos"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Url", typeof(string));
            dt.Columns.Add("PublicId", typeof(string));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = await _photoRepository.GetAll().ToListAsync();

            if (_list.Count > 0) {
                _list.ForEach(item => {
                    dt.Rows.Add(
                        item.Id,
                        item.Url,
                        item.PublicId,
                        item.ProductId,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }
    }
}