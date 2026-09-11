using MoviesBackend.Entities;
using MoviesBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MoviesBackend.Repository
{
    public class MoviesRepository : IRepository<Movie>
    {
        private readonly MoviesContext _context;

        public MoviesRepository(MoviesContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(Movie entity)
        {
            _context.Movies.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            _context.Movies.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Movies.Find(id);
            if (entity != null)
            {
                _context.Movies.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public bool IsExisting(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
