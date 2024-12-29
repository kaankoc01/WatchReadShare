using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Contracts.Persistence
{
    public interface IMovieRepository : IGenericRepository<Movie, int>
    {
    }
}
