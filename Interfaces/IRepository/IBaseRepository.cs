using System.Linq.Expressions;

namespace HNGSTAGETWO.Interfaces.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(string id);
        Task<T> UpdateAsync(T entity);
    }
}
