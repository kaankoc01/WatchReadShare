using WatchReadShare.Application.Features.Comments.Dto;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Contracts.Persistence
{
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
        Task<List<Comment?>> GetCommentsByMovieAsync(int movieId);
        Task<List<Comment?>> GetCommentsBySerialAsync(int serialId);

    }
}
