using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WatchReadShare.Application.Features.Auth;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class RegisterController(IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // Register
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            var values = await authService.RegisterAsync(registerDto);
            return RedirectToAction("Index", "ConfirmMail");

        }
    }
}
