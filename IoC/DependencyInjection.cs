using Application.Handler;
using Application.Helpers;
using Application.Interfaces.Auth;
using Application.Interfaces.Negocio;
using Application.Services.Auth;
using Application.Services.Graficos;
using Application.Services.Negocio;
using Infrastructure.Data.Auth;
using Infrastructure.Data.Negocio;
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

        services.AddScoped<ISistemaRepository, SistemaRepository>();
        services.AddScoped<SistemaService>();

        services.AddScoped<IGraficoRepository, GraficoRepository>();
        services.AddScoped<GraficoService>();

        services.AddScoped<IEventoRepository, EventoRepository>();
        services.AddScoped<EventoService>();

        services.AddScoped<ITiposGraficosHandler, TiemposEntreEventos>();
        return services;
    }
}