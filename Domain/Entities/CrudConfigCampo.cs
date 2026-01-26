using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CrudConfigCampo
{
    public int IdCampo { get; set; }

    public int IdCrudConfig { get; set; }

    public string NombreCampo { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual CrudConfig IdCrudConfigNavigation { get; set; }
}
