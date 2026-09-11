using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Entities;

namespace MoviesBackend.Interfaces
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetAllAsync();

        Task<Movie?> GetByIdAsync(int id);

        Task CreateAsync(Movie movie);

        Task UpdateAsync(Movie movie);

        Task DeleteAsync(int id);

        bool IsExisting(int id);
    }
}
