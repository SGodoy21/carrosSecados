using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Secadora
{
    public long Id { get; set; }

    public string TagNfc { get; set; }

    public string Nombre { get; set; }

    public int Direccion { get; set; }

    public int IdRed { get; set; }

    public int? IdNave { get; set; }

    public string Mensaje { get; set; }

    public int? TempActual { get; set; }

    public int? TempAmbiente { get; set; }

    public int? SpSecado { get; set; }

    public int? Histeresis { get; set; }

    public bool? FirmwareNuevo { get; set; }

    public string Controlador { get; set; }

    public string HsSecar { get; set; }

    public string HsRestante { get; set; }

    public DateTime? FechaComSecado { get; set; }

    public DateTime? FechaFinSecado { get; set; }

    public bool? Leyendo { get; set; }

    public bool? Escribiendo { get; set; }

    public bool? Habilitado { get; set; }

    public bool? Local { get; set; }

    public bool? ContErrores { get; set; }

    public bool? QuemMarcha { get; set; }

    public bool? Rearranque { get; set; }

    public bool? HabilitarSecado { get; set; }

    public bool? Secando { get; set; }

    public int? TipoSecado { get; set; }

    public decimal? HumInicial { get; set; }

    public decimal? HumFinal { get; set; }

    public decimal? PuntosXHoras { get; set; }

    public decimal? PuntosABajar { get; set; }

    public decimal? Muestra { get; set; }

    public bool? NormalAbiertoVent { get; set; }

    public bool? NormalAbiertoQuem { get; set; }

    public virtual ICollection<BocaSecadora> BocasSecadoras { get; set; } = new List<BocaSecadora>();

    public virtual ICollection<HistorialEstado> HistorialsEstados { get; set; } = new List<HistorialEstado>();

    public virtual ICollection<SecadoraCarro> SecadorasCarros { get; set; } = new List<SecadoraCarro>();
}
