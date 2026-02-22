using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MenuItemRol
{
    public int MenuItemId { get; set; }
    public long RolId { get; set; }

    public virtual MenuItem MenuItem { get; set; } = null!;
    public virtual Rol Rol { get; set; } = null!;
}
