using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category,int>
    {
        Task<Category?> GetCategoryWithMovieAsync(int id);
        Task<List<Category?>> GetCategoryWithMovieAsync();
        Task<Category?> GetCategoryByNameAsync(string name);

    }
}
