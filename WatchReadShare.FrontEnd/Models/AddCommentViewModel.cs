using System.ComponentModel.DataAnnotations;

namespace WatchReadShare.FrontEnd.Models
{
    public class AddCommentViewModel
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Lütfen yorumunuzu yazın.")]
        [MinLength(10, ErrorMessage = "Yorum en az 10 karakter olmalıdır.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Lütfen bir puan verin.")]
        [Range(1, 5, ErrorMessage = "Puan 1-5 arasında olmalıdır.")]
        public int Rating { get; set; }
    }
} 