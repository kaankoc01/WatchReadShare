namespace WatchReadShare.Application.Features.Comments.Dto
{
    public class CommentDto()
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int? SerialId { get; set; }
        public int? MovieId { get; set; }
        public int? PostId { get; set; }

    }
    
}
