using Shared.DTOs.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Carro
{
    public class CarroRequestDto
    {
        public int NumeroCarro {  get; set; }
        public string TagNFC { get; set; }
        public string Lote {  get; set; }
    }

    public class CarroResponseDto
    {
        public long Id { get; set; }
        public int NumeroCarro { get; set; }
        public string TagNFC { get; set; }
        public string Lote { get; set; }

        public EstadoActualDto EstadoActual { get; set; }
    }
}
