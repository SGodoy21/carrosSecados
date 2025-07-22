using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

/// <summary>
/// Repository interface for accessing user data.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByAliasAsync(string alias);
    Task<User?> GetByAliasAndPasswordAsync(string alias, string password);
    Task<bool> AddAsync(User newUser);
    Task UpdateAsync(User user);

}
