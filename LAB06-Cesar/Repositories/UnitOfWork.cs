using LAB06_Cesar.Models;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LAB06_Cesar.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable _repositories;
    private readonly Lab06DbContext _context;

    public UnitOfWork(Lab06DbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
        {
            return (IGenericRepository<TEntity, TKey>)_repositories[type];
        }

        var repositoryType = typeof(GenericRepository<,>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity, TKey>)repositoryInstance;
        }
        
        throw new Exception($"Could not create repository instance for type {type}");
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }
}