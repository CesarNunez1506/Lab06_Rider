using LAB06_Cesar.Models;
using LAB06_Cesar.Repositories;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB06_Cesar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere autenticación para todos los endpoints
    public class CursosController : ControllerBase
    {
        private readonly IGenericRepository<Curso, ulong> _cursoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CursosController(IGenericRepository<Curso, ulong> cursoRepository, IUnitOfWork unitOfWork)
        {
            _cursoRepository = cursoRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/cursos
        [HttpGet]
        [Authorize(Roles = "Admin")] // Solo administradores pueden ver todos los cursos
        public async Task<ActionResult<IEnumerable<CursoResponseDTO>>> GetCursos()
        {
            var cursos = await _cursoRepository.GetAllAsync();
            var response = cursos.Select(c => new CursoResponseDTO
            {
                IdCurso = c.IdCurso,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Creditos = c.Creditos
            }).ToList();

            return Ok(response);
        }

        // GET: api/cursos/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")] // Tanto Admin como User pueden ver un curso específico
        public async Task<ActionResult<CursoResponseDTO>> GetCurso(ulong id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound("Curso no encontrado.");
            }

            var response = new CursoResponseDTO
            {
                IdCurso = curso.IdCurso,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                Creditos = curso.Creditos
            };

            return Ok(response);
        }

        // POST: api/cursos
        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo administradores pueden crear cursos
        public async Task<ActionResult<CursoResponseDTO>> CreateCurso(CursoDTO cursoDto)
        {
            var curso = new Curso
            {
                Nombre = cursoDto.Nombre,
                Descripcion = cursoDto.Descripcion,
                Creditos = cursoDto.Creditos
            };

            var createdCurso = await _cursoRepository.InsertAsync(curso);

            var response = new CursoResponseDTO
            {
                IdCurso = createdCurso.IdCurso,
                Nombre = createdCurso.Nombre,
                Descripcion = createdCurso.Descripcion,
                Creditos = createdCurso.Creditos
            };

            return CreatedAtAction(nameof(GetCurso), new { id = response.IdCurso }, response);
        }

        // PUT: api/cursos/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Solo administradores pueden actualizar cursos
        public async Task<IActionResult> UpdateCurso(ulong id, CursoDTO cursoDto)
        {
            var existingCurso = await _cursoRepository.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound("Curso no encontrado.");
            }

            existingCurso.Nombre = cursoDto.Nombre;
            existingCurso.Descripcion = cursoDto.Descripcion;
            existingCurso.Creditos = cursoDto.Creditos;

            _cursoRepository.Update(existingCurso);
            await _unitOfWork.Complete(); // Replace SaveChangesAsync with UnitOfWork.Complete

            return NoContent();
        }

        // DELETE: api/cursos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Solo administradores pueden eliminar cursos
        public async Task<IActionResult> DeleteCurso(ulong id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound("Curso no encontrado.");
            }

            await _cursoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}