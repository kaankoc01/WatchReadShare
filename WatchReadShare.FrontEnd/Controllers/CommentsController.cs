using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WatchReadShare.FrontEnd.Models;
using System.Net.Http.Headers;

namespace WatchReadShare.FrontEnd.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CommentsController> logger)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCommentViewModel model)
        {
            try
            {
                _logger.LogInformation($"Create comment called with MovieId: {model.MovieId}, Content: {model.Content}");

                var token = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { success = false, message = "Oturum süresi dolmuş." });
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { success = false, message = "Kullanıcı bilgisi bulunamadı." });
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
                    return Json(new { success = true });
                }

                return BadRequest(new { success = false, message = responseContent });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Bir hata oluştu." });
            }
        }

        [HttpPost("Like/{id}")]
        public async Task<IActionResult> Like(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { success = false, message = "Oturum süresi dolmuş." });
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Comments/{id}/like", null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return BadRequest(new { success = false, message = responseContent });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Like: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Bir hata oluştu." });
            }
        }
    }
} 