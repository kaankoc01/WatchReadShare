using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WatchReadShare.Application.Features.Auth.Create;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class RegisterController(IHttpClientFactory httpClientFactory) : Controller
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
            // HttpClientHandler ile SSL doğrulamasını atlıyoruz.
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // IHttpClientFactory kullanarak client'ı oluşturuyoruz ve custom handler ekliyoruz.
            var client = new HttpClient(clientHandler);
            var jsondata = JsonConvert.SerializeObject(registerDto); //eklerken serialize
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Auth/Register", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ConfirmMail");
            }

            return View();
            //return RedirectToAction("Index", "ConfirmMail");

        }
    }
}
