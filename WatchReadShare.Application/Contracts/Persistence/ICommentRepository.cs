using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Contracts.Persistence
{
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
    }
}
