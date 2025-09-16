using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
