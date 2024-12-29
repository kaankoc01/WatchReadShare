using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Domain.Entities
{
    public class Genre : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>(); // Tür ile ilişkilendirilen filmler
        public ICollection<Serial> Serials { get; set; } = new List<Serial>(); // Tür ile ilişkilendirilen diziler

    }
}
