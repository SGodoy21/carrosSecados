using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Rol
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<MenuItem> MenusItems { get; set; } = new List<MenuItem>();
}
