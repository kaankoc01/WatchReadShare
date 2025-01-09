namespace WatchReadShare.FrontEnd.Models
{
    public class MyProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public DateTime JoinDate { get; set; }
        public int TotalReviews { get; set; }
        public int TotalLikes { get; set; }
        public List<UserReviewViewModel> Reviews { get; set; } = new();
        public List<WatchlistItemViewModel> Watchlist { get; set; } = new();
    }

    public class UserReviewViewModel
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string MovieImage { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikeCount { get; set; }
    }

    public class WatchlistItemViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; } // İzlendi, İzlenecek, İzleniyor
        public DateTime AddedDate { get; set; }
    }
} 