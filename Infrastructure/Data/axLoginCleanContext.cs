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

    public virtual DbSet<Filtro> Filtros { get; set; }

    public virtual DbSet<FiltroCampo> FiltrosCampos { get; set; }

    public virtual DbSet<FiltroOptionSource> FiltrosOptionsSources { get; set; }

    public virtual DbSet<MenuItem> MenusItems { get; set; }

    public virtual DbSet<MenuItemRol> MenusItemsRoles { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axLoginCleanEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filtro>(entity =>
        {
            entity.HasKey(e => e.IdFiltro).HasName("PK__Filtro__0772E7B29B3FB121");

            entity.ToTable("Filtro");

            entity.HasIndex(e => e.Nombre, "UQ__Filtro__75E3EFCF3CFC1C8E").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ButtonClear).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FiltroCampo>(entity =>
        {
            entity.HasKey(e => e.IdCampo).HasName("PK__FiltroCa__6C61DA81E076F4D1");

            entity.ToTable("FiltroCampo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Conditions)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Format)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.OptionsSourceCode)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PlaceHolder)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFiltroNavigation).WithMany(p => p.FiltrosCampos)
                .HasForeignKey(d => d.IdFiltro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FiltroCampo_Filtro");
        });

        modelBuilder.Entity<FiltroOptionSource>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__FiltroOp__A25C5AA6238B4B4E");

            entity.ToTable("FiltroOptionsSource");

            entity.Property(e => e.Code)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CacheSeconds).HasDefaultValue(120);
            entity.Property(e => e.SourceType)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.Label).HasMaxLength(100);
        });

        modelBuilder.Entity<MenuItemRol>(entity =>
        {
            entity.HasKey(e => new { e.MenuItemId, e.RolId });

            entity.ToTable("MenuItemRoles");
        });

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
