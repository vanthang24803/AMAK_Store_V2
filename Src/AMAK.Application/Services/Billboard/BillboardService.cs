using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cloudinary;
using AMAK.Application.Services.Billboard.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Billboard {

    public class BillboardService : IBillboardService {
        private readonly IRepository<Domain.Models.Billboard> _billboardRepository;

        private readonly ICloudinaryService _cloudinaryService;

        private readonly IMapper _mapper;

        public BillboardService(IRepository<Domain.Models.Billboard> billboardRepository, ICloudinaryService cloudinaryService, IMapper mapper) {
            _billboardRepository = billboardRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task<Response<BillboardResponse>> CreateAsync(IFormFile file, CreateBillboardRequest request) {
            var upload = await _cloudinaryService.UploadPhotoAsync(file);

            if (upload.Error != null) {
                throw new BadRequestException(message: upload.Error.Message);
            }

            var newBillboard = new Domain.Models.Billboard() {
                Id = Guid.NewGuid(),
                Thumbnail = upload.SecureUrl.AbsoluteUri,
                Url = request.Url,
                PublicId = upload.PublicId
            };

            _billboardRepository.Add(newBillboard);

            await _billboardRepository.SaveChangesAsync();

            return new Response<BillboardResponse>(HttpStatusCode.Created, _mapper.Map<BillboardResponse>(newBillboard));
        }

        public async Task<Response<string>> DeleteAsync(Guid id) {
            var existingBillboard = await _billboardRepository.GetById(id) ?? throw new NotFoundException("Billboard not found");

            var deleted = await _cloudinaryService.DeletePhotoAsync(existingBillboard.PublicId!);

            if (deleted.Error != null) {
                throw new BadRequestException(deleted.Error.Message);
            }

            _billboardRepository.Remove(existingBillboard);

            await _billboardRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Billboard deleted successfully!");
        }

        public async Task<Response<List<BillboardResponse>>> GetAllAsync() {
            var billboards = await _billboardRepository.GetAll().ToListAsync();
            return new Response<List<BillboardResponse>>(HttpStatusCode.OK, _mapper.Map<List<BillboardResponse>>(billboards));
        }
    }
}