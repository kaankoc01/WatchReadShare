using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Persistence.Categories;
using WatchReadShare.Persistence.Comments;
using WatchReadShare.Persistence.Genres;
using WatchReadShare.Persistence.Movies;
using WatchReadShare.Persistence.RabbitMQ;
using WatchReadShare.Persistence.Serials;

namespace WatchReadShare.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        // Veritabanı işlemleri. //veritabanı varsa scoped olur. yoksa singleton olur.
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection string'i appsettings.json'dan alıyoruz
            var connectionString = configuration.GetConnectionString("SqlServer");

            // DbContext'i servis koleksiyonuna ekliyoruz
            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(connectionString) // SQL Server'ı kullanarak bağlan
            );
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISerialRepository, SerialRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddSingleton<RabbitMqProducer>();
            services.AddSingleton<RabbitMqConsumer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();




            return services;
        }
       
    }
}
