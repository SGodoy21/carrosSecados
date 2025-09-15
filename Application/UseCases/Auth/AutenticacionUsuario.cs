using Application.Interfaces.Auth;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

public class AutenticacionUsuario(IUsuarioRepository userRepository, IPasswordService passwordService, IConfiguration configuration)
{
    private readonly IUsuarioRepository _usuarioRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Validates the user's credentials.
    /// </summary>
    /// <param name="alias">NombreUsuario (alias).</param>
    /// <param name="password">Plain password.</param>
    /// <returns>The authenticated user or null if invalid.</returns>
    public async Task<Usuario?> ExecuteAsync(string alias, string password)
    {
        Console.WriteLine($"Server UTC now: {DateTime.UtcNow}");
        var expirationDays = _configuration.GetValue<int>("JwtSettings:ExpirationInDays", 30);

        var user = await _usuarioRepository.GetByUsernameAsync(alias);
        if (user == null || !user.Habilitado)
            return null;

        // Validar la contraseña hasheada
        if (!_passwordService.Verificar(password, user.PasswordHash))
            return null;

        // Set token expiration (prefer UtcNow)
        user.FechaCreacion = DateTime.UtcNow;
        user.FechaExpiracion = DateTime.UtcNow.AddDays(expirationDays);
        await _usuarioRepository.UpdateAsync(user);
        return user;
    }
}