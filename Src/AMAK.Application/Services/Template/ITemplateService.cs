using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Template.Dtos;

namespace AMAK.Application.Services.Template {
    public interface ITemplateService {
        Task<Response<string>> CreateAsync(TemplateRequest request);
        Task<Response<string>> UpdateAsync(Guid templateId, TemplateRequest request);
        Task<Response<List<TemplateResponse>>> GetAll();
    }
}