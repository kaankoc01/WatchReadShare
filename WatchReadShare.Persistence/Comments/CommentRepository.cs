using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Comments
{
    public class CommentRepository(Context context) : GenericRepository<Comment, int>(context), ICommentRepository
    {
    }
}
