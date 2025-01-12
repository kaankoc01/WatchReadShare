using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WatchReadShare.FrontEnd.Models;
using System.Net.Http.Headers;
using WatchReadShare.Application;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<MovieController> logger)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                _logger.LogInformation($"Token: {token}");

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                _logger.LogInformation($"API URL: {apiBaseUrl}/Movies/{id}");

                var response = await _httpClient.GetAsync($"{apiBaseUrl}/Movies/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"API Response: {content}");

                    var result = JsonSerializer.Deserialize<ServiceResult<MovieDetailViewModel>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (result != null && result.Data != null)
                    {
                        // ViewBag'e kullanıcı bilgilerini ekle
                        ViewBag.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                        ViewBag.UserName = User.Identity?.Name;
                        ViewBag.IsAuthenticated = User.Identity?.IsAuthenticated ?? false;

                        return View(result.Data);
                    }
                    else
                    {
                        TempData["Error"] = "Film bulunamadı.";
                        return RedirectToAction("Index", "Home");
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Detail action: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            try
            {
                _logger.LogInformation($"AddComment called with MovieId: {model.MovieId}, Content: {model.Content}");

                var token = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("Token not found in session");
                    return RedirectToAction("Index", "Login");
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("UserId not found in claims");
                    return RedirectToAction("Index", "Login");
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var commentRequest = new
                {
                    Content = model.Content,
                    MovieId = model.MovieId,
                    UserId = int.Parse(userId)
                };

                var json = JsonSerializer.Serialize(commentRequest);
                _logger.LogInformation($"Request JSON: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Comments", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Yorumunuz başarıyla eklendi.";
                }
                else
                {
                    _logger.LogWarning($"API error: {response.StatusCode} - {responseContent}");
                    TempData["Error"] = $"Yorum eklenemedi: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddComment: {ex.Message}");
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Detail), new { id = model.MovieId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Comments/{commentId}/like", null);
                return response.IsSuccessStatusCode ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in LikeComment: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}