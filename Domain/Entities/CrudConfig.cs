using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CrudConfig
{
    public int IdCrudConfig { get; set; }

    public string Title { get; set; }

    public string NombreClave { get; set; }

    public string ImagenCampo { get; set; }

    public string CrearTexto { get; set; }

    public bool MostrarEditar { get; set; }

    public bool MostrarHabilitar { get; set; }

    public string FormRoute { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<CrudConfigAccion> CrudsConfigsAccions { get; set; } = new List<CrudConfigAccion>();

    public virtual ICollection<CrudConfigCampo> CrudsConfigsCampos { get; set; } = new List<CrudConfigCampo>();

    public virtual ICollection<CrudConfigEncabezado> CrudsConfigsEncabezados { get; set; } = new List<CrudConfigEncabezado>();
}
