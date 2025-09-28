using System;
using System.Collections.Generic;

namespace LAB06_Cesar.Models;

public partial class Matricula
{
    public ulong IdMatricula { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public string? Semestre { get; set; }
}
