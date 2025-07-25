using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

public class AutenticacionUsuario
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordService _passwordService;

    public AutenticacionUsuario(IUsuarioRepository userRepository, IPasswordService passwordService)
    {
        _usuarioRepository = userRepository;
        _passwordService = passwordService;
    }

    /// <summary>
    /// Validates the user's credentials.
    /// </summary>
    /// <param name="alias">NombreUsuario (alias).</param>
    /// <param name="password">Plain password.</param>
    /// <returns>The authenticated user or null if invalid.</returns>
    public async Task<Usuario?> ExecuteAsync(string alias, string password)
    {
        Console.WriteLine($"Server UTC now: {DateTime.UtcNow}");

        var user = await _usuarioRepository.GetByUsernameAsync(alias);
        if (user == null || !user.Habilitado)
            return null;

        // Validar la contraseña hasheada
        if (!_passwordService.Verificar(password, user.PasswordHash))
            return null;

        // Set token expiration (prefer UtcNow)
        user.FechaCreacion = DateTime.UtcNow;
        user.FechaExpiracion = DateTime.UtcNow.AddMinutes(120);
        await _usuarioRepository.UpdateAsync(user);
        return user;
    }
}
