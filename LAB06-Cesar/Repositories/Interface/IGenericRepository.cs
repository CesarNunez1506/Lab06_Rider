using System.Linq.Expressions;
namespace LAB06_Cesar.Repositories.Interface;

public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    
    // Inserts
    Task<TEntity> InsertAsync(TEntity entity);
    void InsertWithoutSave(TEntity entity);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    
    // Updates
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    
    // Deletes
    Task<bool> DeleteAsync(TKey id);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    
    // Otros métodos útiles
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> AsQueryable();
}