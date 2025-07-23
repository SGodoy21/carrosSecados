using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// Repository interface for accessing user data.
    /// </summary>
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByAliasAsync(string alias);
        Task<User> GetByAliasAndPasswordAsync(string alias, string password);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> AddAsync(User newUser);
        Task UpdateAsync(User user);
        Task<List<User>> GetUsersByRoleIdAsync(int roleId);
    }
}
