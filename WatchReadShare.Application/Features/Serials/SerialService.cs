using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Serials.Create;
using WatchReadShare.Application.Features.Serials.Dto;
using WatchReadShare.Application.Features.Serials.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Serials
{
    public class SerialService(ISerialRepository serialRepository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : ISerialService
    {
        public async Task<ServiceResult<SerialDto?>> GetByIdAsync(int id)
        {
            var serials = await serialRepository.GetByIdAsync(id);
            if (serials is null)
            {
                return ServiceResult<SerialDto?>.Fail("Dizi Bulunamadı.");
            }
            var serialDto = mapper.Map<SerialDto>(serials);
            return ServiceResult<SerialDto?>.Success(serialDto);
        }

        public async Task<ServiceResult<List<SerialDto>>> GetAllListAsync()
        {
            var serials = await serialRepository.GetAllAsync();
            var serialDto = mapper.Map<List<SerialDto>>(serials);
            return ServiceResult<List<SerialDto>>.Success(serialDto);
        }

        public async Task<ServiceResult<List<SerialDto>>> GetPagedAllList(int pageNumber, int pageSize)
        {
            var serials = await serialRepository.GetAllPagedAsync(pageNumber, pageSize);
            var serialDto = mapper.Map<List<SerialDto>>(serials);
            return ServiceResult<List<SerialDto>>.Success(serialDto);
        }

        public async Task<ServiceResult<CreateSerialResponse>> CreateAsync(CreateSerialRequest request)
        {
            var serialCategory = await categoryRepository.GetCategoryByNameAsync("Dizi");
            if (serialCategory is null)
            {
                throw new Exception("Dizi kategorisi bulunamadı.");
            }

            var anySerial = await serialRepository.AnyAsync(x => x.Name == request.Name);
            if (anySerial)
            {
                return ServiceResult<CreateSerialResponse>.Fail("Bu isimde bir dizi zaten var.");
            }
            var serial = mapper.Map<Serial>(request);
            serial.CategoryId = serialCategory.Id;
            await serialRepository.AddAsync(serial);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateSerialResponse>.SuccessAsCreated(new CreateSerialResponse(serial.Id), $"api/serial/{serial.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateSerialRequest request)
        {
            var existingSerial = await serialRepository.GetByIdAsync(request.Id);
            if (existingSerial == null)
            {
                return ServiceResult.Fail("Dizi bulunamadı.", HttpStatusCode.NotFound);
            }

            var isSerialNameExist = await serialRepository.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (isSerialNameExist)
            {
                return ServiceResult.Fail("Bu isimde bir dizi zaten var.", HttpStatusCode.BadRequest);
            }

            existingSerial.Name = request.Name;
            existingSerial.Description = request.Description;
            existingSerial.GenreId = request.GenreId;

            serialRepository.Update(existingSerial);
            await unitOfWork.SaveChangesAsync();
            
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var serials = await serialRepository.GetByIdAsync(id);
            if (serials is null)
            {
                return ServiceResult.Fail("Dizi Bulunamadı.");
            }
            serialRepository.Delete(serials);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
