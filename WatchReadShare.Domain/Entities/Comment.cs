using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Domain.Entities
{
    public class Comment : BaseEntity<int> , IAuditEntity // Yorumlar
    {
        
            public string Content { get; set; } // Yorum içeriği
            public DateTime Created { get; set; } // Oluşturulma tarihi


            public int? MovieId { get; set; } // Film Id'si
            public Movie Movie { get; set; }

            public int? SerialId { get; set; } // Dizi Id'si
            public Serial Serial { get; set; }

            public string UserId { get; set; } // Yorum yapan kullanıcı

            public DateTime? Updated { get; set; } // Güncellenme tarihi
    }
}
