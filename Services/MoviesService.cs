using MoviesBackend.Entities;
using MoviesBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Repository;

namespace MoviesBackend.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IRepository<Movie> _repository;
        private readonly IWebHostEnvironment _environment;

        public MoviesService(IRepository<Movie> repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Movie movie)
        {
            if (movie.ImageFile != null && movie.ImageFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(movie.ImageFile.FileName);
                string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await movie.ImageFile.CopyToAsync(stream);
                }

                movie.Img = "/images/" + fileName;
            }
            else if (string.IsNullOrWhiteSpace(movie.Img))
            {
                movie.Img = "/images/placeholder.jpg";
            }

            await _repository.CreateAsync(movie);
        }

        public async Task UpdateAsync(Movie movie)
        {
            var existing = await _repository.GetByIdAsync(movie.Id);

            if (movie.ImageFile != null && movie.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(existing.Img)
                    && existing.Img.StartsWith("/images/")
                    && existing.Img != "/images/placeholder.jpg")
                {
                    string oldPath = Path.Combine(_environment.WebRootPath, existing.Img.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(movie.ImageFile.FileName);
                string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await movie.ImageFile.CopyToAsync(stream);
                }

                movie.Img = "/images/" + fileName;
            }
            else if (string.IsNullOrWhiteSpace(movie.Img))
            {
                movie.Img = existing.Img;
            }
            await _repository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            if (movie == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(movie.Img)
                && movie.Img.StartsWith("/images/")
                && movie.Img != "/images/placeholder.jpg")
            {
                string filePath = Path.Combine(_environment.WebRootPath, movie.Img.TrimStart('/'));
                if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
            }

            await _repository.DeleteAsync(id);
        }

        public bool IsExisting(int id)
        {
            return _repository.IsExisting(id);
        }
    }
}
