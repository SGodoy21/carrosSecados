using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Services;
using Application.Services.Auth;
using Infrastructure.Data.Auth;
using Infrastructure.Repositories;
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
        services.AddScoped<UsuarioService>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<RolService>();
        services.AddScoped<IFiltroRepository, FiltroRepository>();
        services.AddScoped<FiltroService>();

        return services;
    }
}