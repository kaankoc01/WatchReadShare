namespace WatchReadShare.Application.Features.Movies.Dto;

public record MovieDto(int Id, string Name, string Description, string? ImageUrl, int CategoryId, int GenreId, int? Year);

