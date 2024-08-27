
using AMAK.Application.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Product.Commands.Import {
    public class ImportExcelToProductCommand : IRequest<Response<string>> {

        public IFormFile File { get; set; }

        public ImportExcelToProductCommand(IFormFile file) {
            File = file;
        }
    }
}