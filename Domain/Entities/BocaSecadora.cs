using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BocaSecadora
{
    public long Id { get; set; }

    public long IdSecadora { get; set; }

    public string Posicion { get; set; }

    public virtual Secadora IdSecadoraNavigation { get; set; }
}
