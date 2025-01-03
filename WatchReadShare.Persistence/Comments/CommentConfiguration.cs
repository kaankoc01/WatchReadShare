using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Comments
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.AppUser)
                .WithMany() // AppUser birçok yoruma sahip olabilir
                .HasForeignKey(x => x.UserId) // Foreign key olarak UserId kullanılır
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse yorumlar silinmez

        }
    }
}
