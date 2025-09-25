using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Evento
{
    public long Id { get; set; }

    public int IdFuente { get; set; }

    public int IdTipoEvento { get; set; }

    public DateTime Fecha { get; set; }

    public string DatoJs { get; set; }

    public int Valor { get; set; }

    public string Unidad { get; set; }

    public virtual Fuente IdFuenteNavigation { get; set; }

    public virtual TipoDeEvento IdTipoEventoNavigation { get; set; }
}
