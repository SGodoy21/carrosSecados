using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class GrupoSistema
{
    public int Id { get; set; }

    public int IdGrupo { get; set; }

    public int IdSistema { get; set; }

    public virtual Grupo IdGrupoNavigation { get; set; }

    public virtual Sistema IdSistemaNavigation { get; set; }
}
