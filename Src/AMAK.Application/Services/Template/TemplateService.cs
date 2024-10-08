using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Template.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Template {
    public class TemplateService : ITemplateService {
        private readonly IRepository<EmailTemplate> _templateRepository;

        public TemplateService(IRepository<EmailTemplate> templateRepository) {
            _templateRepository = templateRepository;
        }

        public async Task<Response<string>> CreateAsync(TemplateRequest request) {
            var newTemplate = new EmailTemplate() {
                Id = Guid.NewGuid(),
                Template = request.Template,
                Name = request.Type,
            };

            _templateRepository.Add(newTemplate);

            await _templateRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.Created, "Created Template Successfully!");
        }

        public async Task<Response<List<TemplateResponse>>> GetAll() {
            var templates = await _templateRepository.GetAll().ToListAsync();

            var result = templates.Select(x => new TemplateResponse() {
                Id = x.Id,
                Template = x.Template,
                Type = x.Name
            }).ToList();

            return new Response<List<TemplateResponse>>(HttpStatusCode.OK, result);
        }

        public async Task<Response<string>> UpdateAsync(Guid templateId, TemplateRequest request) {
            var existingTemplate = await _templateRepository.GetAll().FirstOrDefaultAsync(x => x.Id == templateId)
                ?? throw new NotFoundException("Template not found!");

            existingTemplate.Name = request.Type;
            existingTemplate.Template = request.Template;

            _templateRepository.Update(existingTemplate);

            await _templateRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Template updated successfully!");
        }
    }
}