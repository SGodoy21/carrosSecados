using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sistema
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Fuente> Fuentes { get; set; } = new List<Fuente>();

    public virtual ICollection<Grafico> Graficos { get; set; } = new List<Grafico>();

    public virtual ICollection<GrupoSistema> GruposSistemas { get; set; } = new List<GrupoSistema>();

    public virtual ICollection<TipoDeEvento> TiposDesEventos { get; set; } = new List<TipoDeEvento>();
}
