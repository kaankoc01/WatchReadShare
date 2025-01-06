using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Categories;
using WatchReadShare.Application.Features.Categories.Create;
using WatchReadShare.Application.Features.Categories.Update;

namespace WatchReadShare.API.Controllers
{
    [Authorize]
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("movies")]
        public async Task<IActionResult> GetCategoryWithMovie() => CreateActionResult(await categoryService.GetCategoryWithMovieAsync());

        [HttpGet("{id:int}/movies")]
        public async Task<IActionResult> GetCategoryWithMovie(int id) => CreateActionResult(await categoryService.GetCategoryWithMovieAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) => CreateActionResult(await categoryService.CreateAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request) => CreateActionResult(await categoryService.UpdateAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.DeleteAsync(id));
    }
}
