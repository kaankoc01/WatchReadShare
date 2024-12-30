using AutoMapper;
using WatchReadShare.Application.Features.Categories.Create;
using WatchReadShare.Application.Features.Categories.Dto;
using WatchReadShare.Application.Features.Categories.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryWithMovieDto, Category>().ReverseMap();
        }
    }
}
