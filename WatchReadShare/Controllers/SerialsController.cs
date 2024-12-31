using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Serials;
using WatchReadShare.Application.Features.Serials.Create;
using WatchReadShare.Application.Features.Serials.Update;

namespace WatchReadShare.API.Controllers
{
   
    public class SerialsController(ISerialService serialService) : CustomBaseController
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await serialService.GetByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await serialService.GetAllListAsync());
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await serialService.GetPagedAllList(pageNumber, pageSize));
        [HttpPost]
        public async Task<IActionResult> Create(CreateSerialRequest request) => CreateActionResult(await serialService.CreateAsync(request));
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateSerialRequest request) => CreateActionResult(await serialService.UpdateAsync(request));
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await serialService.DeleteAsync(id));

    }
}
