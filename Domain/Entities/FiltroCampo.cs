using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FiltroCampo
{
    public int IdCampo { get; set; }

    public int IdFiltro { get; set; }

    public string Nombre { get; set; }

    public string Title { get; set; }

    public string PlaceHolder { get; set; }

    public string Format { get; set; }

    public string Type { get; set; }

    public string Conditions { get; set; }

    public string OptionsSourceCode { get; set; }

    public string Size { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual Filtro IdFiltroNavigation { get; set; }
}
