using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Dto;
using WatchReadShare.Application.Features.Comments.Update;

namespace WatchReadShare.Application.Features.Comments
{
    public interface ICommentService
    {
        Task<ServiceResult<CommentDto?>> GetByIdAsync(int id);
        Task<ServiceResult<List<CommentDto>>> GetAllListAsync();
        Task<ServiceResult<List<CommentDto>>> GetPagedAllList(int pageNumber, int pageSize);
        Task<ServiceResult<CreateCommentResponse>> CreateAsync(CreateCommentRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCommentRequest request);
        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult<List<CommentDto>>> GetCommentsByMovieAsync(int movieId);
        Task<ServiceResult<List<CommentDto>>> GetCommentsBySerialAsync(int serialId);
    }
}
