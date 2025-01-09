using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public MovieController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/movies/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var movie = await response.Content.ReadFromJsonAsync<MovieDetailViewModel>();
                    return View(movie);
                }
                
                return NotFound();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen tüm alanları doldurun.";
                return RedirectToAction(nameof(Detail), new { id = model.MovieId });
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/comments", model);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Yorumunuz başarıyla eklendi.";
                }
                else
                {
                    TempData["Error"] = "Yorum eklenirken bir hata oluştu.";
                }
            }
            catch
            {
                TempData["Error"] = "Bir hata oluştu. Lütfen tekrar deneyin.";
            }

            return RedirectToAction(nameof(Detail), new { id = model.MovieId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    $"{_apiBaseUrl}/comments/{commentId}/like", null);
                
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/comments/{commentId}");
                
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }

    public class AddCommentRequest
    {
        public int MovieId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
} 