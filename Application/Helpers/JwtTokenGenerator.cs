

using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helpers;

public class JwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerarToken(Usuario usuario)
    {
        var secretKey = _configuration["JwtSettings:Key"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 120);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
        new Claim(ClaimTypes.Email, usuario.Email ?? ""),
        new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "Usuario"),
        new Claim("NombreApellido", $"{usuario.Nombre} {usuario.Apellido}"),
    };

        if (usuario.GrupoId != null)
            claims.Add(new Claim("GrupoId", usuario.GrupoId.ToString()));

        claims.Add(new Claim("Habilitado", usuario.Habilitado.ToString()));

        var now = DateTime.UtcNow;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now,
            IssuedAt = now,
            Expires = now.AddMinutes(expirationMinutes),
            SigningCredentials = credentials,
            Issuer = issuer,
            Audience = audience
        };


        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

}
