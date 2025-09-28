using System;
using System.Collections.Generic;

namespace LAB06_Cesar.Models;

public partial class Evaluacione
{
    public ulong IdEvaluacion { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public decimal? Calificacion { get; set; }

    public DateOnly? Fecha { get; set; }
}
