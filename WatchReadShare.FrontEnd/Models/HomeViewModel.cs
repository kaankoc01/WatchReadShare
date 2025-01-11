namespace WatchReadShare.FrontEnd.Models
{
    public class HomeViewModel
    {
        public List<MovieCardViewModel> LatestMovies { get; set; } = new();
        public List<MovieCardViewModel> TopRatedMovies { get; set; } = new();
        public List<MovieCardViewModel> PopularMovies { get; set; } = new();
    }
} 