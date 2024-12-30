using AutoMapper;
using WatchReadShare.Application.Features.Movies.Create;
using WatchReadShare.Application.Features.Movies.Dto;
using WatchReadShare.Application.Features.Movies.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Movies
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<CreateMovieRequest, Movie>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<UpdateMovieRequest, Movie>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
