using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Filtro
{
    public int IdFiltro { get; set; }

    public string Nombre { get; set; }

    public bool ButtonClear { get; set; }

    public bool Activo { get; set; }

    public int Orden { get; set; }

    public virtual ICollection<FiltroCampo> FiltrosCampos { get; set; } = new List<FiltroCampo>();
}
