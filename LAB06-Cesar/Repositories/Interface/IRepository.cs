using System.Linq.Expressions;

namespace LAB06_Cesar.Repositories.Interface;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<T> InsertAsync(T entity);
    void InsertWithoutSave(T entity);
    Task InsertRangeAsync(IEnumerable<T> entities);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    Task<bool> DeleteAsync(int id);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryable();
}