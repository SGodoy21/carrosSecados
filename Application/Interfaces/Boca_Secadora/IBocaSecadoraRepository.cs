using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Boca_Secadora
{
    public interface IBocaSecadoraRepository
    {
        Task AddAsync(BocaSecadora bocaSecadora);
        Task UpdateAsync(BocaSecadora bocaSecadora);
        Task<BocaSecadora> GetByIdAsync(long id); 
        Task<IEnumerable<BocaSecadora>> GetAllAsync();
        Task<IEnumerable<BocaSecadora>> GetBocasPorSecadora(long idSecadora); 
    }
}
