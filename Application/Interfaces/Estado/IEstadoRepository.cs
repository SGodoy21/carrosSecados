using Domain.Entities;
using Domain.Enums.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IEstado
{
    public interface IEstadoRepository
    {
        Task<Estado> GetByIdAsync(long id);
        Task<IEnumerable<Estado>> GetByAmbitoAsync(AmbitoEstadoEnum ambito);
    }
}
