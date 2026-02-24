using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Carro
{
    public long Id { get; set; }

    public int NumeroCarro { get; set; }

    public string TagNfc { get; set; }

    public string Lote { get; set; }

    public virtual ICollection<HistorialEstado> HistorialsEstados { get; set; } = new List<HistorialEstado>();

    public virtual ICollection<SecadoraCarro> SecadorasCarros { get; set; } = new List<SecadoraCarro>();
}
