using Microsoft.EntityFrameworkCore;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Categories
{
    public class CategoryRepository(Context context) : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithMovieAsync(int id) => context.Categories.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

        public Task<List<Category?>> GetCategoryWithMovieAsync() => context.Categories.Include(x => x.Movies).ToListAsync();

        public Task<Category?> GetCategoryByNameAsync(string name) => context.Categories.FirstOrDefaultAsync(x => x.Name == name);

    }
}
