using AutoMapper;
using System.Net;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Dto;
using WatchReadShare.Application.Features.Comments.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Comments
{
    public class CommentService(ICommentRepository commentRepository , IUnitOfWork unitOfWork , IMapper mapper) : ICommentService
    {
        public async Task<ServiceResult<CommentDto?>> GetByIdAsync(int id)
        {
           var comment = await commentRepository.GetByIdAsync(id);
            if (comment is null)
            {
                return ServiceResult<CommentDto?>.Fail("Yorum Bulunamadı.", HttpStatusCode.NotFound);
            }
            var commentDto = mapper.Map<CommentDto>(comment);
            return ServiceResult<CommentDto?>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetAllListAsync()
        {
            var comment = await commentRepository.GetAllAsync();
            var commentDto = mapper.Map<List<CommentDto>>(comment);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetPagedAllList(int pageNumber, int pageSize)
        {
            var comments = await commentRepository.GetAllPagedAsync(pageNumber, pageSize);
            var commentDto = mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<CreateCommentResponse>> CreateAsync(CreateCommentRequest request)
        {
            var anyComment = await commentRepository.AnyAsync(x => x.Content == request.Content);
            if (anyComment)
            {
                return ServiceResult<CreateCommentResponse>.Fail("Bu yorumun aynısı zaten var.", HttpStatusCode.BadRequest);
            }
            var comment = mapper.Map<Comment>(request);
            await commentRepository.AddAsync(comment);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateCommentResponse>.SuccessAsCreated(mapper.Map<CreateCommentResponse>(comment), "api/comments");

        }

        public async Task<ServiceResult> UpdateAsync(UpdateCommentRequest request)
        {
            var isCommentNameExist = await commentRepository.AnyAsync(x => x.Content == request.Content && x.Id != request.Id);
            if (isCommentNameExist)
            {
                return ServiceResult.Fail("Bu yorum zaten var.", HttpStatusCode.BadRequest);
            }
            var comment = mapper.Map<Comment>(request);
            comment.Id = request.Id;
            commentRepository.Update(comment);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment is null)
            {
                return ServiceResult.Fail("Yorum Bulunamadı.", HttpStatusCode.NotFound);
            }
            commentRepository.Delete(comment);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetCommentsByMovieAsync(int movieId)
        {
            var comments = await commentRepository.GetCommentsByMovieAsync(movieId);
            var commentDto = mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetCommentsBySerialAsync(int serialId)
        {
            var comments = await commentRepository.GetCommentsBySerialAsync(serialId);
            var commentDto = mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }
    }
}
