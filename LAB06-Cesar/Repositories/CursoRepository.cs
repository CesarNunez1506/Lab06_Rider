using LAB06_Cesar.Models;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAB06_Cesar.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly Lab06DbContext _context;

        public CursoRepository(Lab06DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso> GetByIdAsync(ulong id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Curso> CreateAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            _context.Entry(curso).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Cursos.AnyAsync(e => e.IdCurso == curso.IdCurso))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return false;
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
