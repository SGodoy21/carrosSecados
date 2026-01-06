using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Auth;
using Infrastructure.Repositories;
using IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// ----- Configuraci�n Global -----
Constantes.oConfig = builder.Configuration;

// ----- Services -----
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- Swagger + JWT ---
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "axAnalytics API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT como: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.CustomSchemaIds(type => type.FullName); // (Opcional) evitar conflictos en Swagger
});

// --- CORS (Desarrollo) ---
const string DevCorsPolicy = "DevCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// --- Output Cache (Opcional) ---
builder.Services.AddOutputCache(options =>
    options.AddPolicy("axAnalytics", b => b.Expire(TimeSpan.FromSeconds(30)).Tag("axAnalytics"))
);

// --- EF Core: DbContext ---
builder.Services.AddDbContext<axLoginCleanContext>(options =>
    options.UseSqlServer(Constantes.oConfig.GetConnectionString("axAnalyticsEntities"))
);

// --- IoC: Servicios de tu soluci�n ---
builder.Services.AddProjectServices();

// --- JWT Authentication ---
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSection["Key"] ?? throw new Exception("JwtSettings:Key missing!");
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.UseSecurityTokenValidators = true; // Necesario para par�metros custom
        options.RequireHttpsMetadata = false; // S�lo desarrollo
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            LifetimeValidator = (notBefore, expires, token, parameters) =>
            {
                var now = DateTime.UtcNow;
                return (notBefore == null || now >= notBefore) &&
                       (expires == null || now < expires);
            },
            ClockSkew = TimeSpan.FromMinutes(2),
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("JWT ERROR: " + ctx.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>(); // tu implementación real


var app = builder.Build();

// ----- Middleware pipeline -----
//

app.UseDefaultFiles();   // Busca index.html, default.html, etc. en wwwroot
app.UseStaticFiles();    // Habilita wwwroot

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "axAnalytics API v1");
    options.RoutePrefix = "swagger"; // Mueve swagger a /swagger
});
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseCors(DevCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

app.Run();