using Application.Helpers;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<AutenticacionUsuario>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<JwtTokenGenerator>();
        services.AddSingleton<IPasswordService, BcryptPasswordService>();
        services.AddScoped<Application.Services.UsuarioService>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<Application.Services.RolService>();
        return services;
    }
}
