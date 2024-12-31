using WatchReadShare.Application.Features.Movies.Dto;

namespace WatchReadShare.Application.Features.Categories.Dto;

public record CategoryWithMovieDto(int Id, string Name , List<MovieDto> Movies); 
 
