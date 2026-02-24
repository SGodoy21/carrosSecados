using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Secadora.Boca_Secadora
{
    public class BocaSecadoraRequestDto
    {
        public long IdSecadora { get; set; }
        public string Posicion { get; set; } = null!;
    }

    public class BocaSecadoraResponseDto
    {
        public long Id { get; set; }
        public long IdSecadora { get; set; }
        public string Posicion { get; set; } = null!;
    }
}
