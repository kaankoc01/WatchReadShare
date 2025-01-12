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
                    return RedirectToAction("ConfirmEmail", new { email = registerDto.Email });
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

        [HttpGet]
        public IActionResult ConfirmEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string email, int code)
        {
            try
            {
                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var confirmRequest = new { Email = email, ConfirmCode = code };
                var json = JsonSerializer.Serialize(confirmRequest);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Auth/ConfirmEmail", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "E-posta doğrulama başarılı! Şimdi giriş yapabilirsiniz.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Doğrulama başarısız: {error}";
                    ViewBag.Email = email;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
                ViewBag.Email = email;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResendCode(string email)
        {
            try
            {
                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var request = new { Email = email };
                var json = JsonSerializer.Serialize(request);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Auth/ResendConfirmationCode", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Doğrulama kodu tekrar gönderildi." });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"Hata: {error}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
            }
        }
    }
}

