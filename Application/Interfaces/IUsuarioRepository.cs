using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// Repository interface for accessing user data.
    /// </summary>
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(long userId);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByAliasAsync(string alias);
        Task<Usuario> GetByAliasAndPasswordAsync(string alias, string password);
        Task<Usuario> GetByUsernameAsync(string username);
        Task<bool> AddAsync(Usuario newUser);
        Task UpdateAsync(Usuario user);
        Task<List<Usuario>> GetUsersByRoleIdAsync(int roleId);
        Task DeleteAsync(Usuario user);
    }
}
