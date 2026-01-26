using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CrudConfigAccion
{
    public int IdAccion { get; set; }

    public int IdCrudConfig { get; set; }

    public string Nombre { get; set; }

    public string Icono { get; set; }

    public string Color { get; set; }

    public string Endpoint { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual CrudConfig IdCrudConfigNavigation { get; set; }
}
