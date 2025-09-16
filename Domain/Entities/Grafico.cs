using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Grafico
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public int IdSistema { get; set; }

    public int IdTipoGrafico { get; set; }

    public bool PorDefecto { get; set; }

    public virtual Sistema IdSistemaNavigation { get; set; }

    public virtual TipoDeGrafico IdTipoGraficoNavigation { get; set; }
}
