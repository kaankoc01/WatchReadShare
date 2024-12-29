using WatchReadShare.Application.Features.Categories.Create;
using WatchReadShare.Application.Features.Categories.Dto;
using WatchReadShare.Application.Features.Categories.Update;

namespace WatchReadShare.Application.Features.Categories
{
    public interface ICategoryService
    {
        Task<CategoryWithMovieDto?> GetCategoryWithMovieAsync(int id);
        Task<List<CategoryWithMovieDto?>> GetCategoryWithMovieAsync();

        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
