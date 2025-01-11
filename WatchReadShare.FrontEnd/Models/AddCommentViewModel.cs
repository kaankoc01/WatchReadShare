using System.ComponentModel.DataAnnotations;

namespace WatchReadShare.FrontEnd.Models
{
    public class AddCommentViewModel
    {
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? MovieId { get; set; }
        public int? SerialId { get; set; }
    }
} 