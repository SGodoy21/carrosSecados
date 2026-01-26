using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class CrudConfigEncabezadosDto
    {
        public int idEncabezado { get; set; }
        public int idCrudConfig { get; set; }
        public string texto { get; set; }
        public int orden { get; set; }
        public bool activo { get; set; }

    }
}
