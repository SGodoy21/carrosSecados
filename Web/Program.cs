using Application.Helpers;
using Infrastructure.Data;
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
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// ----- Configuración Global -----
Constantes.oConfig = builder.Configuration;

// ----- Controllers & Endpoints -----
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ----- Swagger + JWT -----
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginClean API", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT como: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    o.CustomSchemaIds(type => type.FullName);
});

// ----- CORS (Solo para desarrollo) -----
const string devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// ----- Output Cache (opcional) -----
builder.Services.AddOutputCache(options =>
    options.AddPolicy("LoginClean", b => b.Expire(TimeSpan.FromSeconds(30)).Tag("LoginClean")));

// ----- EF Core -----
builder.Services.AddDbContext<LoginCleanContext>(options =>
{
    options.UseSqlServer(Constantes.oConfig.GetConnectionString("axLoginCleanEntities"));
});

// ----- IoC -----
builder.Services.AddProjectServices();

// ----- JWT Authentication -----
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSection["Key"] ?? throw new Exception("JwtSettings:Key missing!");
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.UseSecurityTokenValidators = true;
        options.RequireHttpsMetadata = false;
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
            // Usar DateTime.Now en vez de UtcNow para la validación (NO RECOMENDADO)
            LifetimeValidator = (notBefore, expires, token, parameters) =>
            {
                var now = DateTime.UtcNow;
                return (notBefore == null || now >= notBefore) &&
                       (expires == null || now < expires);
            },
            ClockSkew = TimeSpan.FromMinutes(2)
        };
        // Si querés, podés agregar OnAuthenticationFailed para debug.
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("JWT ERROR: " + ctx.Exception.Message);
                Console.WriteLine("=== TokenValidationParameters ===");
                Console.WriteLine($"ValidateIssuerSigningKey: {options.TokenValidationParameters.ValidateIssuerSigningKey}");
                Console.WriteLine($"IssuerSigningKey: {options.TokenValidationParameters.IssuerSigningKey}");
                Console.WriteLine($"ValidateIssuer: {options.TokenValidationParameters.ValidateIssuer}");
                Console.WriteLine($"ValidIssuer: {options.TokenValidationParameters.ValidIssuer}");
                Console.WriteLine($"ValidateAudience: {options.TokenValidationParameters.ValidateAudience}");
                Console.WriteLine($"ValidAudience: {options.TokenValidationParameters.ValidAudience}");
                Console.WriteLine($"ValidateLifetime: {options.TokenValidationParameters.ValidateLifetime}");
                Console.WriteLine($"RequireExpirationTime: {options.TokenValidationParameters.RequireExpirationTime}");
                Console.WriteLine($"ClockSkew: {options.TokenValidationParameters.ClockSkew}");
                Console.WriteLine("===============================");

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ----- Middleware -----
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginClean API v1");
    c.RoutePrefix = "";
});
app.UseStaticFiles();
app.UseRouting();
app.UseCors(devCorsPolicy);
// [Opcional] Debug del header JWT en consola:
app.Use(async (context, next) =>
{
    var rawHeader = context.Request.Headers["Authorization"].ToString();
    //var jwt = context.Request.Headers["Authorization"].Replace("Bearer ", string.Empty);
    if (!string.IsNullOrEmpty(rawHeader))
    {
        Console.WriteLine("HEADER Authorization: " + rawHeader);
        if (rawHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var onlyToken = rawHeader.Substring("Bearer ".Length).Trim();
            Console.WriteLine("TOKEN EXTRAÍDO: " + onlyToken);
            Console.WriteLine("TOKEN tiene puntos? " + (onlyToken.Contains('.') ? "SÍ" : "NO"));
        }
    }
    await next();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();
app.MapControllers();

app.Run();
