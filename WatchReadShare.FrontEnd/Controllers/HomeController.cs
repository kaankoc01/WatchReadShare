using Microsoft.AspNetCore.Mvc;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 