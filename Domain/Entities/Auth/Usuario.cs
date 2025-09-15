using System;

namespace Domain.Entities.Auth;

/// <summary>
/// Domain entity representing a system user.
/// </summary>
public class Usuario
{
    public long Id { get; set; }

    public string NombreUsuario { get; set; } = default!;
    public string Nombre { get; set; } = default!;
    public string Apellido { get; set; } = default!;
    public string Telefono { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public bool Habilitado { get; set; } = true;
    public int AccesosIncorrectos { get; set; }

    public int GrupoId { get; set; }
    public long ClienteId { get; set; }

    public string RecoveryToken { get; set; } = default!;
    public DateTime? FechaExpiracion { get; set; }
    public DateTime FechaCreacion { get; set; }

    // FK
    public int RolId { get; set; }

    public Rol Rol { get; set; } = default!;
}