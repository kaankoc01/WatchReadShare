using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WatchReadShare.Application.Features.Auth.Dtos;
using WatchReadShare.Application.Features.Token;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class LoginController(IHttpClientFactory httpClientFactory) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //Login
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            // HttpClientHandler ile SSL doğrulamasını atlıyoruz.
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // IHttpClientFactory kullanarak client'ı oluşturuyoruz ve custom handler ekliyoruz.
            var client = new HttpClient(clientHandler);
            

            var jsondata = JsonConvert.SerializeObject(loginDto); //eklerken serialize
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Auth/Login", stringContent);
           
            if (responseMessage.IsSuccessStatusCode)
            {
                var tokenResponse = await responseMessage.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);

                // Session kullanarak token'ı saklama
                // HttpContext.Session.SetString("AccessToken", token.AccessToken);

                // Alternatif: Cookie kullanımı
                Response.Cookies.Append("AccessToken", token.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = token.ExpiresAt
                });

                return RedirectToAction("Index", "MyProfile");
            }

            return View();


        }
    }
}
