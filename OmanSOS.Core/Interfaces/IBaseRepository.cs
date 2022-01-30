namespace OmanSOS.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<int> AddAsync(T entity);

    Task<int> CountAsync();

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task<int> DeleteAsync(int id);

    Task<int> UpdateAsync(int id, T entity);
}
