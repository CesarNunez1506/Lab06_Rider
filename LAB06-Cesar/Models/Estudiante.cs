using System;
using System.Collections.Generic;

namespace LAB06_Cesar.Models;

public partial class Estudiante
{
    public ulong IdEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }
}
