using System.Net;
using AutoMapper;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Categories.Create;
using WatchReadShare.Application.Features.Categories.Dto;
using WatchReadShare.Application.Features.Categories.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository , IUnitOfWork unitOfWork , IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CategoryWithMovieDto>> GetCategoryWithMovieAsync(int id)
        {
            var category = await categoryRepository.GetCategoryWithMovieAsync(id);
            if (category is null)
            {
                return ServiceResult<CategoryWithMovieDto>.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryWithMovieDto>(category);
            return ServiceResult<CategoryWithMovieDto>.Success(categoryDto);
        }

        public async Task<ServiceResult<List<CategoryWithMovieDto>>> GetCategoryWithMovieAsync()
        {
            var category = await categoryRepository.GetCategoryWithMovieAsync();

            var categoryDto = mapper.Map<List<CategoryWithMovieDto>>(category);
            return ServiceResult<List<CategoryWithMovieDto>>.Success(categoryDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            var categoryDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoryDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.AnyAsync(x => x.Name == request.Name);

            if (anyCategory)
            {
                return ServiceResult<int>.Fail("Kategori ismi veri tabanında bulunmaktadır..", HttpStatusCode.BadRequest);
            }
            var newCategory = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<int>.Success(newCategory.Id);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
        {
            var isCategoryNameExist = await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Kategori ismi veri tabanında bulunmaktadır..", HttpStatusCode.BadRequest);
            }

            var category = mapper.Map<Category>(request);
            category.Id = request.Id;
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);

        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
            }
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
