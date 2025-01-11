using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WatchReadShare.Application.Features.Auth.Dtos;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RegisterController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //Register
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            try
            {
                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var json = JsonSerializer.Serialize(registerDto);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Auth/Register", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen e-posta adresinize gönderilen doğrulama kodunu girin.";
                    return RedirectToAction("Index", "ConfirmMail", new { email = registerDto.Email });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Kayıt başarısız: {error}");
                    return View(registerDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                return View(registerDto);
            }
        }



    }
}

