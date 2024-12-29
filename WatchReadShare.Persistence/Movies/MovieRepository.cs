using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Movies
{
    public class MovieRepository(Context context) : GenericRepository<Movie, int>(context), IMovieRepository
    {
    }
}
