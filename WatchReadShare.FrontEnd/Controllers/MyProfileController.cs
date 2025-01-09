using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    // [Authorize(AuthenticationSchemes = "Cookies")]
    public class MyProfileController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7113/api";

        public MyProfileController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AddBearerToken();
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/User/GetProfile");

                if (response.IsSuccessStatusCode)
                {
                    var profile = await response.Content.ReadFromJsonAsync<MyProfileViewModel>();
                    return View(profile);
                }

                return View(new MyProfileViewModel()); // Boş model ile view'ı göster
            }
            catch
            {
                return View(new MyProfileViewModel()); // Hata durumunda da boş model ile view'ı göster
            }
        }

        private void AddBearerToken()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
