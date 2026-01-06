using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Label { get; set; }   
        public string Icon { get; set; }
        public string RouterLink { get; set; }
        public int ParentId { get; set; }
        public int Orden { get; set; }  
        public bool Activo { get; set; }

    }
}
