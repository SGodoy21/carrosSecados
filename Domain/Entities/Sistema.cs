using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sistema
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Grafico> Graficos { get; set; } = new List<Grafico>();
}
