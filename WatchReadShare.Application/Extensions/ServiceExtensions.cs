using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchReadShare.Application.Features.Categories;
using WatchReadShare.Application.Features.Comments;
using WatchReadShare.Application.Features.Genres;
using WatchReadShare.Application.Features.Movies;
using WatchReadShare.Application.Features.Serials;

namespace WatchReadShare.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISerialService, SerialService>();

          
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;

        }
    }
}
