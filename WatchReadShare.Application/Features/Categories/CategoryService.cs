using AutoMapper;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Categories.Create;
using WatchReadShare.Application.Features.Categories.Dto;
using WatchReadShare.Application.Features.Categories.Update;

namespace WatchReadShare.Application.Features.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository , IUnitOfWork unitOfWork , IMapper mapper) : ICategoryService
    {
        public Task<CategoryWithMovieDto?> GetCategoryWithMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryWithMovieDto?>> GetCategoryWithMovieAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
