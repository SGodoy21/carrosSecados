using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
    Task<Role> GetByIdAsync(int id);
    Task<Role> GetByNameAsync(string name);
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
}
