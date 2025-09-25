using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class ConfiguracionesGraficosDto
    {
      
    }

    public class JsConfiguracionTiempo
    {
        public int IdFuenteInicio { get; set; }
        public int IdTipoEventoInicio { get; set; }
        public int IdFuenteFin { get; set; }
        public int IdTipoEventoFin { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int PlazoHs { get; set; }
        public bool Promedio { get; set; }
        public int AgrupadoPor { get; set; }

    }
}
