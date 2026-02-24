using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ISecadora
{
    public interface ISecadoraRepository
    {
        Task AddAsync(Secadora secadora);
        Task UpdateAsync(Secadora secadora);
        Task<Secadora> GetByIdAsync(long id);
        Task<Secadora> GetByNameAsync(string nombre); 
        Task<IEnumerable<Secadora>> GetAllAsync();
    }
}
