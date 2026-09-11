using Microsoft.EntityFrameworkCore;
using MoviesBackend.Entities;
using MoviesBackend.Repository;
using MoviesBackend.Interfaces;

namespace MoviesBackend.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IRepository<Movie>, MoviesRepository>();

            return services;
        }
    }
}
