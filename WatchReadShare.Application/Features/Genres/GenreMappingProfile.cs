using AutoMapper;
using WatchReadShare.Application.Features.Genres.Create;
using WatchReadShare.Application.Features.Genres.Dto;
using WatchReadShare.Application.Features.Genres.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Genres
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<CreateGenreRequest, Genre>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<UpdateGenreRequest, Genre>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        }
    }
}
