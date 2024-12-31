using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Movies;
using WatchReadShare.Application.Features.Movies.Create;
using WatchReadShare.Application.Features.Movies.Update;

namespace WatchReadShare.API.Controllers
{

    public class MoviesController(IMovieService movieService) : CustomBaseController
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await movieService.GetByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await movieService.GetAllListAsync());
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await movieService.GetPagedAllList(pageNumber, pageSize));
        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieRequest request) => CreateActionResult(await movieService.CreateAsync(request));
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateMovieRequest request) => CreateActionResult(await movieService.UpdateAsync(request));
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await movieService.DeleteAsync(id));

    }
}
