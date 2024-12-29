using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence.Serials
{
    public class SerialConfiguration : IEntityTypeConfiguration<Serial>
    {
        public void Configure(EntityTypeBuilder<Serial> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        }
    
    }
}
