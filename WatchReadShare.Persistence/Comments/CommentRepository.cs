using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace WatchReadShare.Persistence.Comments
{
    public class CommentRepository(Context context) : GenericRepository<Comment, int>(context), ICommentRepository
    {
        public async Task<List<Comment?>> GetCommentsByMovieAsync(int movieId)
        {
            return await
                context.Comments
                    .Include(c => c.AppUser) // Yorumu yapan kullanıcıyı da dahil etmek için.
                    .Where(c => c.MovieId == movieId)
                    .ToListAsync();
        }


        public async Task<List<Comment?>> GetCommentsBySerialAsync(int serialId)
        {
            return await context.Comments
               .Include(c => c.AppUser) // Yorumu yapan kullanıcıyı da dahil etmek için.
               .Where(c => c.SerialId == serialId)
               .ToListAsync();
        }
    }
}
