using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

public class AuthenticateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthenticateUser(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    /// <summary>
    /// Validates the user's credentials.
    /// </summary>
    /// <param name="alias">Username (alias).</param>
    /// <param name="password">Plain password.</param>
    /// <returns>The authenticated user or null if invalid.</returns>
    public async Task<User?> ExecuteAsync(string alias, string password)
    {
        Console.WriteLine($"Server UTC now: {DateTime.UtcNow}");

        var user = await _userRepository.GetByUsernameAsync(alias);
        if (user == null || !user.IsEnabled)
            return null;

        // Validar la contraseña hasheada
        if (!_passwordService.Verify(password, user.PasswordHash))
            return null;

        // Set token expiration (prefer UtcNow)
        user.CreatedAt = DateTime.UtcNow;
        user.TokenExpiresAt = DateTime.UtcNow.AddMinutes(120);
        await _userRepository.UpdateAsync(user);
        return user;
    }
}
