using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WatchReadShare.FrontEnd.Models;
using System.Net.Security;
using WatchReadShare.Application.Features.Movies.Dto;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.GetAsync($"{apiBaseUrl}/Movies");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movies = JsonSerializer.Deserialize<List<MovieDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var movieViewModels = movies?.Select(m => new MovieCardViewModel
                    {
                        Id = m.Id,
                        Name = m.Name ?? string.Empty,
                        Description = m.Description ?? string.Empty,
                        ImageUrl = m.ImageUrl ?? "/images/default-movie.jpg",
                        Year = m.Year ?? 0
                        //Genre = "Belirtilmemiş"
                        //Rating = 0
                    }).ToList() ?? new List<MovieCardViewModel>();

                    return View(movieViewModels);
                }

                return View(new List<MovieCardViewModel>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View(new List<MovieCardViewModel>());
            }
        }
    }
} 