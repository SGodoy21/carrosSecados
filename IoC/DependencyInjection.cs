using Application.Helpers;
using Application.Interfaces.Auth;
using Application.Services.Auth;
using Infrastructure.Data.Auth;
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

 

        return services;
    }
}