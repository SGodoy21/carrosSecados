using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CrudConfigEncabezado
{
    public int IdEncabezado { get; set; }

    public int IdCrudConfig { get; set; }

    public string Texto { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual CrudConfig IdCrudConfigNavigation { get; set; }
}
