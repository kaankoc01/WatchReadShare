using WatchReadShare.Application.Features.Movies.Create;
using WatchReadShare.Application.Features.Movies.Dto;
using WatchReadShare.Application.Features.Movies.Update;

namespace WatchReadShare.Application.Features.Movies
{
    public interface IMovieService
    {
        Task<ServiceResult<MovieDto?>> GetByIdAsync(int id);
        Task<ServiceResult<List<MovieDto>>> GetAllListAsync();
        Task<ServiceResult<List<MovieDto>>> GetPagedAllList(int pageNumber, int pageSize);
        Task<ServiceResult<CreateMovieResponse>> CreateAsync(CreateMovieRequest request);
        Task<ServiceResult> UpdateAsync(UpdateMovieRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<string?> GetMovieDetailAsync(int id);
    }
}
