using Shared.DTOs.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Secadora
{
    public class SecadoraRequestDto
    {
        public string TagNfc { get; set; }
        public string Nombre { get; set; }
        public int Direccion { get; set; }
        public int IdRed { get; set; }
        public int? IdNave { get; set; }

        public string Controlador { get; set; }
        public bool? Habilitado { get; set; }
        public bool? Local { get; set; }

        public int? SpSecado { get; set; }
        public int? Histeresis { get; set; }
        public int? TipoSecado { get; set; }

        public decimal? HumFinal { get; set; }
        public decimal? PuntosXHoras { get; set; }
        public decimal? PuntosABajar { get; set; }
    }

    public class SecadoraResponseDto
    {
        public long Id { get; set; }

        public string TagNfc { get; set; }
        public string Nombre { get; set; }

        public int Direccion { get; set; }
        public int IdRed { get; set; }
        public int? IdNave { get; set; }

        public string Controlador { get; set; }

        public bool? Habilitado { get; set; }
        public bool? Local { get; set; }

        public bool? Secando { get; set; }
        public string Mensaje { get; set; }

        public DateTime? FechaComSecado { get; set; }
        public DateTime? FechaFinSecado { get; set; }

        public EstadoActualDto EstadoActual { get; set; }
    }

    public class SecadoraRuntimeDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }

        public int? TempActual { get; set; }
        public int? TempAmbiente { get; set; }

        public bool? Secando { get; set; }
        public bool? Leyendo { get; set; }
        public bool? Escribiendo { get; set; }

        public decimal? HumInicial { get; set; }
        public decimal? HumFinal { get; set; }
        public decimal? Muestra { get; set; }

        public string HsRestante { get; set; }
    }

    public class SecadoraDetailDto : SecadoraResponseDto
    {
        public int? TempActual { get; set; }
        public int? TempAmbiente { get; set; }
        public int? SpSecado { get; set; }

        public decimal? HumInicial { get; set; }
        public decimal? HumFinal { get; set; }

        public bool? FirmwareNuevo { get; set; }
    }

}
