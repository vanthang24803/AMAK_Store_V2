
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using MediatR;
using System.Net;

namespace AMAK.Application.Services.Categories.Commands.Delete {
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Domain.Models.Category> categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
            var existCategory = await _categoryRepository.GetById(request.Id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            existCategory.IsDeleted = true;

            await _categoryRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Category deleted successfully!");
        }
    }
}