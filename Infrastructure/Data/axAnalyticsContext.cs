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

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Fuente> Fuentes { get; set; }

    public virtual DbSet<Grafico> Graficos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<GrupoSistema> GruposSistemas { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<TipoDeEvento> TiposDesEventos { get; set; }

    public virtual DbSet<TipoDeGrafico> TiposDesGraficos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axAnalyticsEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.Property(e => e.DatoJs)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Unidad)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFuenteNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdFuente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Eventos_Fuentes");

            entity.HasOne(d => d.IdTipoEventoNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdTipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Eventos_TiposDeEventos");
        });

        modelBuilder.Entity<Fuente>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Fuentes)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fuentes_Fuentes");
        });

        modelBuilder.Entity<Grafico>(entity =>
        {
            entity.Property(e => e.JsConfiguracion)
                .HasMaxLength(500)
                .IsUnicode(false);
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

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GrupoSistema>(entity =>
        {
            entity.Property(e => e.IdSistema).HasColumnName("IdSIstema");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.GruposSistemas)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GruposSistemas_Grupos");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.GruposSistemas)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GruposSistemas_Sistemas");
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

        modelBuilder.Entity<TipoDeEvento>(entity =>
        {
            entity.ToTable("TiposDeEventos");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.TiposDesEventos)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TiposDeEventos_Sistemas");
        });

        modelBuilder.Entity<TipoDeGrafico>(entity =>
        {
            entity.ToTable("TiposDeGraficos");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.JsConfiguracion)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
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
