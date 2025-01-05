using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WatchReadShare.Application.Features.Auth;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class RegisterController(UserManager<AppUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;
                code = random.Next(100000, 999999);

                AppUser appUser = new AppUser
                {
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    ConfirmCode = code
                };
                var result = await userManager.CreateAsync(appUser, registerDto.Password);
                if (result.Succeeded)
                {
                    MimeMessage message = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("WatchReadShare-Admin","kaankoc01@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress(appUser.Name, appUser.Email);

                    message.From.Add(mailboxAddressFrom);
                    message.To.Add(mailboxAddressTo);
                    

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt İşlemini Gerçekleştirmek için Onay Kodunuz : " + code; 
                    message.Body = bodyBuilder.ToMessageBody();
                    message.Subject = "WatchReadShare Onay Kodu";

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("kaankoc01@gmail.com", "nhvtydbvceljhpan");
                    client.Send(message);
                    client.Disconnect(true);

                    TempData["Mail"] = appUser.Email;
                    return RedirectToAction("Index", "ConfirmMail");


                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View();
        }
    }
}
