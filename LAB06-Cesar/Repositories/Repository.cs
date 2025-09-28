using System.Linq.Expressions;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LAB06_Cesar.Repositories.Interface;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) 
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<T> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public void InsertWithoutSave(T entity) => _dbSet.Add(entity);

    public async Task InsertRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity) => _dbSet.Update(entity);
    public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;
        
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public void Delete(T entity) => _dbSet.Remove(entity);
    public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) 
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        => predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) 
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public IQueryable<T> AsQueryable() => _dbSet.AsQueryable();
}