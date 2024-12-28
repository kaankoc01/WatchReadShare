using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WatchReadShare.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        // Veritabanı işlemleri. 
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection string'i appsettings.json'dan alıyoruz
            var connectionString = configuration.GetConnectionString("SqlServer");

            // DbContext'i servis koleksiyonuna ekliyoruz
            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(connectionString) // SQL Server'ı kullanarak bağlan
            );




            return services;
        }
       
    }
}
