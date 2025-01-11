namespace WatchReadShare.FrontEnd.Models
{
    public class MyProfileViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public int? ConfirmCode { get; set; }
        public List<UserReviewViewModel> Reviews { get; set; } = new();
    }

    public class UserReviewViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? MovieId { get; set; }
        public int? SerialId { get; set; }
        public DateTime Created { get; set; }
        public int LikeCount { get; set; }
    }
} 