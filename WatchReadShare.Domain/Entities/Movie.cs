﻿using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Domain.Entities
{
    public class Movie : BaseEntity<int> , IAuditEntity
    {
        public string Name { get; set; }  // Film adı
        public string Description { get; set; }  // Film açıklaması

        public int GenreId { get; set; } // Tür ID'si
        public Genre Genre { get; set; } = default!; // Tür ile ilişki

        public int CategoryId { get; set; }
        // bir Filmin mutlaka categoriysi olmalı bü yüzden default olamaz işaretledik.
        public Category Category { get; set; } = default!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
