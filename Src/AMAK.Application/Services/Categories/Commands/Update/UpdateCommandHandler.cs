using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Categories.Common;
using AutoMapper;
using MediatR;
using System.Net;

namespace AMAK.Application.Services.Categories.Commands.Update {
    public class UpdateCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<CategoryResponse>> {

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IMapper _mapper;

        public UpdateCommandHandler(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {

            var existCategory = await _categoryRepository.GetById(request.Id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            _mapper.Map(request.Category, existCategory);

            _categoryRepository.Update(existCategory);

            await _categoryRepository.SaveChangesAsync();

            return new Response<CategoryResponse>(HttpStatusCode.OK, _mapper.Map<CategoryResponse>(existCategory));
        }
    }
}