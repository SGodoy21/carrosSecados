using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class CrudConfigCampo
    {
        public int idCampo { get; set; }
        public int idCrudConfig { get; set; }
        public string nombreCampo { get; set; }
        public int orden { get; set; }
        public bool activo { get; set; }
    }
}
