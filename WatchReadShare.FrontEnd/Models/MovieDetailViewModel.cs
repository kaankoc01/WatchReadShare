namespace WatchReadShare.FrontEnd.Models
{
    public class MovieDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();
    }

    public class CommentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
    }
} 