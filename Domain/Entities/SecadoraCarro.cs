using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SecadoraCarro
{
    public long IdSecadora { get; set; }

    public long IdCarro { get; set; }

    public long IdUsuario { get; set; }

    public virtual Carro IdCarroNavigation { get; set; }

    public virtual Secadora IdSecadoraNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
