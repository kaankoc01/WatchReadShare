using Microsoft.AspNetCore.Mvc;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class MyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
