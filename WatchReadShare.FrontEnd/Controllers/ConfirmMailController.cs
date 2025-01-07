using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class ConfirmMailController(IHttpClientFactory httpClientFactory) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.Mail = value;
          //  confirmMailViewModel.Mail = value.ToString();
            return View();
        }
        //ConfirmMail
        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            // HttpClientHandler ile SSL doğrulamasını atlıyoruz.
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // IHttpClientFactory kullanarak client'ı oluşturuyoruz ve custom handler ekliyoruz.
            var client = new HttpClient(clientHandler);
            var jsondata = JsonConvert.SerializeObject(confirmMailViewModel); //eklerken serialize
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Auth/ConfirmEmail", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
