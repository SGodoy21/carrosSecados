namespace Shared.DTOs;

/// <summary>
/// DTO for public user response.
/// </summary>
public class UsuarioDto
{
    public long Id { get; set; }
    public string NombreUsuario { get; set; } = default!;
    public string NombreApellido { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RolId { get; set; }
    public int GrupoId { get; set; }
    public bool Habilitado { get; set; }
}

public class UsuarioLoginDto
{
    public string NombreUsuario { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class UsuarioLoginResponseDto
{
    public UsuarioDto Usuario { get; set; } = default!;
    public string Token { get; set; } = default!;
}

public class CambiarRolUsuarioDto
{
    public long UsuarioId { get; set; }
    public int RolId { get; set; }
    // Opcional: public string ChangedBy { get; set; }
}

public class RegistrarUsuarioDto
{
    public string NombreUsuario { get; set; }
    public string Password { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int GrupoId { get; set; }
    public int clienteId { get; set; }
    // El rol se asigna automáticamente en el backend
}