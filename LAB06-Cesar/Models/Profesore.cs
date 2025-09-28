using System;
using System.Collections.Generic;

namespace LAB06_Cesar.Models;

public partial class Profesore
{
    public ulong IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Especialidad { get; set; }

    public string? Correo { get; set; }
}
