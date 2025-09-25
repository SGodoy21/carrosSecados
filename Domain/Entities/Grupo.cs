using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Grupo
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<GrupoSistema> GruposSistemas { get; set; } = new List<GrupoSistema>();
}
