using System.ComponentModel.DataAnnotations;

namespace LAB06_Cesar.Models
{
    public class CursoDTO
    {
        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El número de créditos es obligatorio.")]
        [Range(1, 10, ErrorMessage = "El número de créditos debe estar entre 1 y 10.")]
        public int Creditos { get; set; }
    }
}