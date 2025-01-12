using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WatchReadShare.FrontEnd.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using WatchReadShare.Application;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MovieController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
            _configuration = configuration;
        }
        // incelenecek
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.GetAsync($"{apiBaseUrl}/Movies/{id}");

   

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movie = JsonConvert.DeserializeObject<ServiceResult<MovieDetailViewModel>>(content);  //sanırım burada sorun 

                    return View(movie);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }
        // incelenecek
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Index", "Login");
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var commentRequest = new
                {
                    MovieId = model.MovieId,
                    Content = model.Content

                };

                var json = JsonSerializer.Serialize(commentRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.PostAsync($"{apiBaseUrl}/Comments", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Yorumunuz başarıyla eklendi.";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Yorum eklenemedi: {error}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Detail), new { id = model.MovieId });
        }
        // like back-endde daha yok.
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
            catch
            {
                return StatusCode(500);
            }
        }
    }

}