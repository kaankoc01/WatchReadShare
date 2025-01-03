using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Comments;
using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Update;

namespace WatchReadShare.API.Controllers
{
   
    public class CommentsController(ICommentService commentService): CustomBaseController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await commentService.GetByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await commentService.GetAllListAsync());
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await commentService.GetPagedAllList(pageNumber, pageSize));
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequest request) => CreateActionResult(await commentService.CreateAsync(request));
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateCommentRequest request) => CreateActionResult(await commentService.UpdateAsync(request));
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await commentService.DeleteAsync(id));
        [HttpGet("movie/{movieId}/comments")]
        public async Task<IActionResult> GetCommentsByMovie(int movieId) => CreateActionResult(await commentService.GetCommentsByMovieAsync(movieId));
        [HttpGet("serial/{serialId}/comments")]
        public async Task<IActionResult> GetCommentsBySerial(int serialId) => CreateActionResult(await commentService.GetCommentsBySerialAsync(serialId));


    }
}
