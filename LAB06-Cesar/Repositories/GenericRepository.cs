using System.Linq.Expressions;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using LAB06_Cesar.Models;
namespace LAB06_Cesar.Repositories;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly Lab06DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(Lab06DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public void InsertWithoutSave(TEntity entity) => _dbSet.Add(entity);

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void UpdateRange(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public void Delete(TEntity entity) => _dbSet.Remove(entity);
    public void DeleteRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        => predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public IQueryable<TEntity> AsQueryable() => _dbSet.AsQueryable();
}