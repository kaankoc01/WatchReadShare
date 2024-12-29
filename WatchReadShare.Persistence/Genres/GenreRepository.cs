using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Genres
{
    public class GenreRepository(Context context) : GenericRepository<Genre, int>(context), IGenreRepository
    {
    }
}
