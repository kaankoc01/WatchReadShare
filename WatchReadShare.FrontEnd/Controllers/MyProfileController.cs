using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchReadShare.Application;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    // [Authorize(AuthenticationSchemes = "Cookies")]
    public class MyProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // incelenecek.***
        public async Task<IActionResult> Index()
        {

            try
            {
                AddBearerToken();
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7113/api/Auth/confirm-email"); // böyle bir api ucu yok User/GetProfile 

                if (response.IsSuccessStatusCode)
                {
                    var profile = await response.Content.ReadAsStringAsync();
                    var movies = JsonConvert.DeserializeObject<ServiceResult<List<MyProfileViewModel>>>(profile);

                    return View(movies);
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
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }


    }
}
