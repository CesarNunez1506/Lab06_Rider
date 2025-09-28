using LAB06_Cesar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAB06_Cesar.Repositories.Interface
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<Curso> GetByIdAsync(ulong id);
        Task<Curso> CreateAsync(Curso curso);
        Task<bool> UpdateAsync(Curso curso);
        Task<bool> DeleteAsync(ulong id);
    }
}
