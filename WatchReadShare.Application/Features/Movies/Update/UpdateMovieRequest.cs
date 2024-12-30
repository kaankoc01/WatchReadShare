namespace WatchReadShare.Application.Features.Movies.Update;

public record UpdateMovieRequest(int Id ,string Name, string Description, int CategoryId, int GenreId);


