using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchReadShare.Application;
using WatchReadShare.Application.Features.Movies.Dto;
using WatchReadShare.FrontEnd.Models;

namespace WatchReadShare.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7113/api/Movies");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movies = JsonConvert.DeserializeObject<ServiceResult<List<MovieDto>>>(content);

                    var movieViewModels = movies?.Data.Select(m => new MovieCardViewModel
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