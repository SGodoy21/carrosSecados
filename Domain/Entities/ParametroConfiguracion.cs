using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametroConfiguracion
{
    public long Id { get; set; }

    public string ClaveNombre { get; set; }

    public string ClaveValor { get; set; }

    public string Descripcion { get; set; }
}
