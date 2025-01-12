using Microsoft.AspNetCore.Mvc;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.ViewComponents
{
    public class AddCommentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int movieId)
        {
            var model = new AddCommentViewModel
            {
                MovieId = movieId
            };
            
            return View(model);
        }
    }
} 