using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

public class AuthenticateUser
{
    private readonly IUserRepository _userRepository;

    public AuthenticateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Validates the user's credentials.
    /// </summary>
    /// <param name="alias">Username (alias).</param>
    /// <param name="password">Plain password.</param>
    /// <returns>The authenticated user or null if invalid.</returns>
    public async Task<User?> ExecuteAsync(string alias, string password)
    {
        var user = await _userRepository.GetByAliasAndPasswordAsync(alias, password);

        if (user == null || !user.IsEnabled)
            return null;

        //// ? Set token expiration (prefer UtcNow)
        user.TokenExpiresAt = DateTime.Now.AddMinutes(120);

        await _userRepository.UpdateAsync(user);

        return user;
    }
}
