using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICarro
{
    public interface ICarroRepository
    {
        Task AddAsync(Carro carro);
        Task UpdateAsync(Carro carro);
        Task<Carro> GetByIdAsync(long id);
        Task<Carro> GetByNumeroCarroAsync(int numCarro);
        Task<IEnumerable<Carro>> GetCarrosPorLotes(string lote); 
        Task<IEnumerable<Carro>> GetAllAsync();

    }
}
