using Application.Helpers;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticateUser>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<JwtTokenGenerator>();
        services.AddSingleton<IPasswordService, BcryptPasswordService>();
        services.AddScoped<Application.Services.UserService>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<Application.Services.RoleService>();
        return services;
    }
}
