using MoviesBackend.Entities;

namespace MoviesBackend.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        bool IsExisting(int id);
    }
}
