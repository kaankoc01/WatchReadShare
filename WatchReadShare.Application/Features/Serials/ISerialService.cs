using WatchReadShare.Application.Features.Serials.Create;
using WatchReadShare.Application.Features.Serials.Dto;
using WatchReadShare.Application.Features.Serials.Update;

namespace WatchReadShare.Application.Features.Serials
{
    public interface ISerialService
    {
        Task<ServiceResult<SerialDto?>> GetByIdAsync(int id);
        Task<ServiceResult<List<SerialDto>>> GetAllListAsync();
        Task<ServiceResult<List<SerialDto>>> GetPagedAllList(int pageNumber, int pageSize);
        Task<ServiceResult<CreateSerialResponse>> CreateAsync(CreateSerialRequest request);
        Task<ServiceResult> UpdateAsync(UpdateSerialRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
