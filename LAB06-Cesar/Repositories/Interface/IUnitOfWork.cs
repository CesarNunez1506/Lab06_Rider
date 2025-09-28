using System.Linq.Expressions;

namespace LAB06_Cesar.Repositories.Interface;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class;
    Task<int> Complete();
    int SaveChanges();
}