namespace WatchReadShare.Application.Features.Movies.Dto
{

    public class MovieDto
    {
        public MovieDto()
        {
            // Boş yapıcı gerektiği için tanımlandı.
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int GenreId { get; set; }
        public int? Year { get; set; }

    }


}