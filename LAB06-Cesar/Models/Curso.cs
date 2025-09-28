using System;
using System.Collections.Generic;

namespace LAB06_Cesar.Models;

public partial class Curso
{
    public ulong IdCurso { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Creditos { get; set; }
}
