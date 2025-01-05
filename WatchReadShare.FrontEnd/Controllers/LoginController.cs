using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Auth;
using WatchReadShare.Domain.Entities;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, true);
            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                if (user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "MyProfile");
                }
                // else lütfen mail adresinizi doğrulayın.  
            }
            // kullanıcı adı veya şifre hatalı
            return View();
        }
    }
}
