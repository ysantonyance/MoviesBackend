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
            var connectionString = configuration.GetConnectionString("MoviesContext") 
                ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.");

            services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IRepository<Movie>, MoviesRepository>();

            return services;
        }
    }
}
