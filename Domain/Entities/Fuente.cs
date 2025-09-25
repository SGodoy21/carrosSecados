using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Fuente
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public int IdSistema { get; set; }

    public string Referencia { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual Sistema IdSistemaNavigation { get; set; }
}
