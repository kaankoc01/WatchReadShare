using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Domain.Entities
{
    public class Serial : BaseEntity<int> , IAuditEntity
    {
        public string Name { get; set; } // Dizi adı
        public string Description { get; set; } // Açıklama

        public int GenreId { get; set; } // Tür ID'si
        public Genre Genre { get; set; } = default!; // Tür ile ilişki

        public int? SeasonsCount { get; set; } // Sezon sayısı
        public int? EpisodesCount { get; set; } // Bölüm sayısı

        public int CategoryId { get; set; }
        // bir dizinin mutlaka categoriysi olmalı büyüzden default olamaz işaretledik.
        public Category Category { get; set; } = default!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
