using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class CrudConfigDto
    {
        public string Title { get; set; }
        public List<CrudConfigEncabezadosDto> Encabezados { get; set; }
        public List<CrudConfigCampo> Campos { get; set; }
        public string NombreClave { get; set; }
        public string? ImagenCampo { get; set; }
        public string Crear { get; set; }
        public bool MostrarEditar { get; set; }
        public bool MostrarHabilitar { get; set; }
        public string FormRoute { get; set; }
        public List<object> Acciones { get; set; }
    }
}
