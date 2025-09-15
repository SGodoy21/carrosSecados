using System.Collections.Generic;

namespace Domain.Entities.Auth;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}