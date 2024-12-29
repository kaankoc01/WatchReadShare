using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence
{
    public class Context(DbContextOptions<Context> options) : IdentityDbContext<AppUser,AppRole,int>(options)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
