using WatchReadShare.Application.Features.Genres.Create;
using WatchReadShare.Application.Features.Genres.Dto;
using WatchReadShare.Application.Features.Genres.Update;

namespace WatchReadShare.Application.Features.Genres
{
    public interface IGenreService
    {
        Task<ServiceResult<GenreDto?>> GetByIdAsync(int id);
        Task<ServiceResult<List<GenreDto>>> GetAllListAsync();
        Task<ServiceResult<List<GenreDto>>> GetPagedAllList(int pageNumber, int pageSize);
        Task<ServiceResult<CreateGenreResponse>> CreateAsync(CreateGenreRequest Request);
        Task<ServiceResult> UpdateAsync(UpdateGenreRequest request);
        Task<ServiceResult> DeleteAsync(int id);

    }
}
