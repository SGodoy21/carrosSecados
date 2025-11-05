using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class axLoginCleanContext : DbContext
{
    public axLoginCleanContext()
    {
    }

    public axLoginCleanContext(DbContextOptions<axLoginCleanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axLoginCleanEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Usuarios_Email").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "IX_Usuarios_NombreUsuario").IsUnique();

            entity.HasIndex(e => e.RolId, "IX_Usuarios_RolId");

            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Habilitado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.RecoveryToken).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
