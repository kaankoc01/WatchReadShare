namespace WatchReadShare.FrontEnd.Models
{
    public class MovieCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int GenreId { get; set; }
        public int? Year { get; set; }
    }
} 