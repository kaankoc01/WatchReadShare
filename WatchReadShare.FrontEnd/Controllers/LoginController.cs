using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Auth;
using WatchReadShare.Domain.Entities;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            var values = await authService.LoginAsync(loginDto);
            return RedirectToAction("Index", "MyProfile");
        }
    }
}
