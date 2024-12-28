using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Persistence
{
    public class Context(DbContextOptions<Context> options) : IdentityDbContext<AppUser,AppRole,int>(options)
    {

        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
