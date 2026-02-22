using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Estado
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public string Ambito { get; set; }

    public virtual ICollection<HistorialEstado> HistorialsEstados { get; set; } = new List<HistorialEstado>();
}
