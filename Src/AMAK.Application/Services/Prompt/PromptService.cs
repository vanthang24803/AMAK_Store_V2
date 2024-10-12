using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Prompt.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Prompt {
    public class PromptService : IPromptService {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Models.Prompt> _promptRepository;

        public PromptService(IRepository<Domain.Models.Prompt> promptRepository, IMapper mapper) {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAsync(PromptRequest request) {
            var isExistingPrompt = await _promptRepository.GetAll().FirstOrDefaultAsync(x => x.Type == request.Type);

            if (isExistingPrompt != null) {
                throw new BadRequestException("Prompt already exists!");
            }

            var newPrompt = _mapper.Map<Domain.Models.Prompt>(request);

            _promptRepository.Add(newPrompt);

            await _promptRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.Created, "Prompt created successfully!");
        }

        public async Task<Response<string>> DeleteAsync(Guid id) {
            var existingPrompt = await _promptRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new NotFoundException("Prompt not found!");

            _promptRepository.Remove(existingPrompt);

            await _promptRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Prompt deleted successfully!");

        }

        public async Task<Response<List<PromptResponse>>> GetAllAsync() {
            var prompts = await _promptRepository.GetAll().ToListAsync();

            var result = _mapper.Map<List<PromptResponse>>(prompts);

            return new Response<List<PromptResponse>>(HttpStatusCode.OK, result);
        }

        public async Task<Response<PromptResponse>> GetOneAsync(Guid id) {
            var existingPrompt = await _promptRepository.GetAll()
                 .FirstOrDefaultAsync(x => x.Id == id)
                 ?? throw new NotFoundException("Prompt not found!");

            var result = _mapper.Map<PromptResponse>(existingPrompt);

            return new Response<PromptResponse>(HttpStatusCode.OK, result);
        }

        public async Task<Response<string>> UpdateAsync(Guid id, PromptRequest request) {
            var existingPrompt = await _promptRepository.GetAll()
               .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new NotFoundException("Prompt not found!");

            _mapper.Map(request, existingPrompt);

            _promptRepository.Update(existingPrompt);

            await _promptRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Prompt updated successfully!");
        }
    }
}