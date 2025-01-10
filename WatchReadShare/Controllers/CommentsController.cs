using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Comments;
using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Update;
using System.Security.Claims;

namespace WatchReadShare.API.Controllers
{

   // [Authorize]
    public class CommentsController : CustomBaseController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            // Token'dan userId'yi al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            // UserId'yi request'e ekle
            var commentRequest = new CreateCommentRequest(
                Content: request.Content,
                UserId: int.Parse(userId),
                MovieId: request.MovieId,
                SerialId: request.SerialId
            );

            var result = await _commentService.CreateAsync(commentRequest);
            return CreateActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _commentService.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _commentService.GetAllListAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => 
            CreateActionResult(await _commentService.GetPagedAllList(pageNumber, pageSize));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateCommentRequest request) => 
            CreateActionResult(await _commentService.UpdateAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => 
            CreateActionResult(await _commentService.DeleteAsync(id));

        [HttpGet("movie/{movieId}/comments")]
        public async Task<IActionResult> GetCommentsByMovie(int movieId) => 
            CreateActionResult(await _commentService.GetCommentsByMovieAsync(movieId));

        [HttpGet("serial/{serialId}/comments")]
        public async Task<IActionResult> GetCommentsBySerial(int serialId) => 
            CreateActionResult(await _commentService.GetCommentsBySerialAsync(serialId));
    }
}
