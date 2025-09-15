using Domain.Entities.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRolRepository
{
    Task<List<Rol>> GetAllAsync();

    Task<Rol> GetByIdAsync(int id);

    Task<Rol> GetByNombreAsync(string nombre);

    Task AddAsync(Rol rol);

    Task UpdateAsync(Rol rol);

    Task DeleteAsync(Rol rol);
}