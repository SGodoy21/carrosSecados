using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HistorialEstado
{
    public long Id { get; set; }

    public long IdSecadora { get; set; }

    public long? IdCarro { get; set; }

    public long IdEstado { get; set; }

    public DateTime FechaHoraInicio { get; set; }

    public DateTime? FechaHoraFin { get; set; }

    public virtual Carro IdCarroNavigation { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; }

    public virtual Secadora IdSecadoraNavigation { get; set; }
}
