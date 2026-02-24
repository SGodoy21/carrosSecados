using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Secadora_Carro
{
    public class AsignarSecadoraCarroRequestDto
    {
        public long SecadoraId { get; set; }
        public long CarroId { get; set; }
    }

    public class FinalizarAsignacionRequestDto
    {
        public long SecadoraId { get; set; }
        public long CarroId { get; set; }
    }
    public class AsignacionActivaDto
    {
        public long SecadoraId { get; set; }
        public string SecadoraNombre { get; set; }

        public long? CarroId { get; set; }
        public int? NumeroCarro { get; set; }

        public bool TieneCarro => CarroId.HasValue;
    }

    public class SecadoraCarroResponseDto
    {
        public long SecadoraId { get; set; }
        public string SecadoraNombre { get; set; }

        public long CarroId { get; set; }
        public int NumeroCarro { get; set; }

        public long UsuarioId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public bool Activa => FechaFin == null;
    }
}
