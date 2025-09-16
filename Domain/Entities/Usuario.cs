using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Usuario
{
    public long Id { get; set; }

    public string NombreUsuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Telefono { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool Habilitado { get; set; }

    public int AccesosIncorrectos { get; set; }

    public int GrupoId { get; set; }

    public long ClienteId { get; set; }

    public string RecoveryToken { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int RolId { get; set; }

    public virtual Rol Rol { get; set; }
}
