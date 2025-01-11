namespace WatchReadShare.FrontEnd.Models
{
    public class MovieDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int GenreId { get; set; }
        public int? Year { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();
    }

    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? MovieId { get; set; }
        public int? SerialId { get; set; }
        public DateTime Created { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
    }
} 