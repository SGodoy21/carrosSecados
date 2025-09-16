using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class axAnalyticsContext : DbContext
{
    public axAnalyticsContext()
    {
    }

    public axAnalyticsContext(DbContextOptions<axAnalyticsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grafico> Graficos { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<TipoDeGrafico> TiposDesGraficos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axAnalyticsEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grafico>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Graficos)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Graficos_Sistemas");

            entity.HasOne(d => d.IdTipoGraficoNavigation).WithMany(p => p.Graficos)
                .HasForeignKey(d => d.IdTipoGrafico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Graficos_TiposDeGraficos");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDeGrafico>(entity =>
        {
            entity.ToTable("TiposDeGraficos");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
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
