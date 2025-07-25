using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data;

public class LoginCleanContext : DbContext
{
    public LoginCleanContext(DbContextOptions<LoginCleanContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // USERS
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.Habilitado).HasDefaultValue(true);
            entity.Property(e => e.AccesosIncorrectos).HasDefaultValue(0);
            entity.Property(e => e.GrupoId);
            entity.Property(e => e.ClienteId);
            entity.Property(e => e.RecoveryToken).HasMaxLength(255);
            entity.Property(e => e.FechaExpiracion);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");

            entity.HasIndex(e => e.NombreUsuario).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasOne(e => e.Rol)
                  .WithMany(r => r.Usuarios)
                  .HasForeignKey(e => e.RolId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ROLES
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Roles");

            entity.HasKey(r => r.Id);
            entity.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Rol>().HasData(
       new Rol { Id = 1, Nombre = "Admin" },
       new Rol { Id = 2, Nombre = "Usuario" }
   );

        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                NombreUsuario = "Aumax",
                Email = "admin@aumax.com.ar",
                PasswordHash = "$2a$11$PYZWVVI5oRiGqd8cYLbi7eOpDAb171A2Eqv1VAfhMVzRtCrCwxoku", // IMPORTANTE: usá hash real
                Nombre = "Aumax",
                Apellido = "SRL",
                Telefono = "1234567890",
                Habilitado = true,
                AccesosIncorrectos = 0,
                GrupoId = 1,
                ClienteId = 1,
                RecoveryToken = "",
                FechaExpiracion = null,
                FechaCreacion = DateTime.UtcNow,
                RolId = 1
            }
        );

    }
}
