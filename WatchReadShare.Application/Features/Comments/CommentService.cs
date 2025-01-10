using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Dto;
using WatchReadShare.Application.Features.Comments.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Comments
{
    public class CommentService(ICommentRepository commentRepository,
                         IMovieRepository movieRepository,
                         ISerialRepository serialRepository,
                         IUnitOfWork unitOfWork,
                         IMapper mapper,
                         UserManager<AppUser> userManager,
                         SignInManager<AppUser> signInManager) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly ISerialRepository _serialRepository = serialRepository;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResult<CommentDto?>> GetByIdAsync(int id)
        {
           var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
            {
                return ServiceResult<CommentDto?>.Fail("Yorum Bulunamadı.", HttpStatusCode.NotFound);
            }
            var commentDto = _mapper.Map<CommentDto>(comment);
            return ServiceResult<CommentDto?>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetAllListAsync()
        {
            var comment = await _commentRepository.GetAllAsync();
            var commentDto = _mapper.Map<List<CommentDto>>(comment);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetPagedAllList(int pageNumber, int pageSize)
        {
            var comments = await _commentRepository.GetAllPagedAsync(pageNumber, pageSize);
            var commentDto = _mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<CreateCommentResponse>> CreateAsync(CreateCommentRequest request)
        {
            try
            {
                // Kullanıcı kontrolü
                
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return ServiceResult<CreateCommentResponse>.Fail("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
                }

                // Film veya dizi kontrolü
                if (request.MovieId.HasValue)
                {
                    var movie = await _movieRepository.GetByIdAsync(request.MovieId.Value);
                    if (movie == null)
                    {
                        return ServiceResult<CreateCommentResponse>.Fail("Film bulunamadı.", HttpStatusCode.NotFound);
                    }
                }
                else if (request.SerialId.HasValue)
                {
                    var serial = await _serialRepository.GetByIdAsync(request.SerialId.Value);
                    if (serial == null)
                    {
                        return ServiceResult<CreateCommentResponse>.Fail("Dizi bulunamadı.", HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return ServiceResult<CreateCommentResponse>.Fail("Film veya dizi ID'si gereklidir.", HttpStatusCode.BadRequest);
                }

                var comment = _mapper.Map<Comment>(request);
                comment.UserId = request.UserId;
                comment.Created = DateTime.UtcNow;

                await _commentRepository.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult<CreateCommentResponse>.SuccessAsCreated(
                    new CreateCommentResponse(comment.Id), 
                    $"api/comments/{comment.Id}");
            }
            catch (Exception ex)
            {
                return ServiceResult<CreateCommentResponse>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCommentRequest request)
        {
            var isCommentNameExist = await _commentRepository.AnyAsync(x => x.Content == request.Content && x.Id != request.Id);
            if (isCommentNameExist)
            {
                return ServiceResult.Fail("Bu yorum zaten var.", HttpStatusCode.BadRequest);
            }
            var comment = _mapper.Map<Comment>(request);
            comment.Id = request.Id;
            _commentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
            {
                return ServiceResult.Fail("Yorum Bulunamadı.", HttpStatusCode.NotFound);
            }
            _commentRepository.Delete(comment);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetCommentsByMovieAsync(int movieId)
        {
            var comments = await _commentRepository.GetCommentsByMovieAsync(movieId);
            var commentDto = _mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

        public async Task<ServiceResult<List<CommentDto>>> GetCommentsBySerialAsync(int serialId)
        {
            var comments = await _commentRepository.GetCommentsBySerialAsync(serialId);
            var commentDto = _mapper.Map<List<CommentDto>>(comments);
            return ServiceResult<List<CommentDto>>.Success(commentDto);
        }

    }
}
