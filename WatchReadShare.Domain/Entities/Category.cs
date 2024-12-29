using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!; // Kategori adı

        public ICollection<Serial> Serials { get; set; } = new List<Serial>(); // Dizilerle ilişki
        public ICollection<Movie> Movies { get; set; } = new List<Movie>(); // Filmlerle ilişki


        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
