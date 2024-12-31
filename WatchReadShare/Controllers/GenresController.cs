using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Genres;
using WatchReadShare.Application.Features.Genres.Create;
using WatchReadShare.Application.Features.Genres.Update;

namespace WatchReadShare.API.Controllers
{
    public class GenresController(IGenreService genreService) : CustomBaseController
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await genreService.GetByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await genreService.GetAllListAsync());
        [HttpGet("{pageNumber:int}/ {pageSize:int}")]
        public async Task<IActionResult> GetPage(int pageNumber, int pageSize) => CreateActionResult(await genreService.GetPagedAllList(pageNumber, pageSize));
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreRequest request) => CreateActionResult(await genreService.CreateAsync(request));
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateGenreRequest request) => CreateActionResult(await genreService.UpdateAsync(request));
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await genreService.DeleteAsync(id));
    }
}
