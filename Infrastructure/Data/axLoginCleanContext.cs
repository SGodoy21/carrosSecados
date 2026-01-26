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

    public virtual DbSet<CrudConfig> CrudsConfigs { get; set; }

    public virtual DbSet<CrudConfigAccion> CrudsConfigsAccions { get; set; }

    public virtual DbSet<CrudConfigCampo> CrudsConfigsCampos { get; set; }

    public virtual DbSet<CrudConfigEncabezado> CrudsConfigsEncabezados { get; set; }

    public virtual DbSet<Filtro> Filtros { get; set; }

    public virtual DbSet<FiltroCampo> FiltrosCampos { get; set; }

    public virtual DbSet<FiltroOptionSource> FiltrosOptionsSources { get; set; }

    public virtual DbSet<MenuItem> MenusItems { get; set; }

    public virtual DbSet<MenuItemRol> MenusItemsRoles { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<UiFilterConfig> UisFiltersConfigs { get; set; }

    public virtual DbSet<UiFilterField> UisFiltersFields { get; set; }

    public virtual DbSet<UiOptionItem> UisOptionsItems { get; set; }

    public virtual DbSet<UiOptionList> UisOptionsLists { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axLoginCleanEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrudConfig>(entity =>
        {
            entity.HasKey(e => e.IdCrudConfig).HasName("PK__CrudConf__C512F5759ACC0D7F");

            entity.ToTable("CrudConfig");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CrearTexto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FormRoute)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImagenCampo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreClave)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CrudConfigAccion>(entity =>
        {
            entity.HasKey(e => e.IdAccion).HasName("PK__CrudConf__9845169B6EF329B9");

            entity.ToTable("CrudConfigAccion");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Endpoint)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCrudConfigNavigation).WithMany(p => p.CrudsConfigsAccions)
                .HasForeignKey(d => d.IdCrudConfig)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrudConfigAccion_Config");
        });

        modelBuilder.Entity<CrudConfigCampo>(entity =>
        {
            entity.HasKey(e => e.IdCampo).HasName("PK__CrudConf__6C61DA813D9BE3B9");

            entity.ToTable("CrudConfigCampo");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.NombreCampo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCrudConfigNavigation).WithMany(p => p.CrudsConfigsCampos)
                .HasForeignKey(d => d.IdCrudConfig)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrudConfigCampo_Config");
        });

        modelBuilder.Entity<CrudConfigEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdEncabezado).HasName("PK__CrudConf__6309B948872688C7");

            entity.ToTable("CrudConfigEncabezado");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Texto)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCrudConfigNavigation).WithMany(p => p.CrudsConfigsEncabezados)
                .HasForeignKey(d => d.IdCrudConfig)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrudConfigEncabezado_Config");
        });

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

        modelBuilder.Entity<UiFilterConfig>(entity =>
        {
            entity.ToTable("UiFilterConfig");

            entity.HasIndex(e => e.Code, "UQ_UiFilterConfig_Code").IsUnique();

            entity.Property(e => e.ApplyLabel).HasMaxLength(50);
            entity.Property(e => e.ClearLabel).HasMaxLength(50);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ShowApplyButton).HasDefaultValue(true);
            entity.Property(e => e.ShowClearButton).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<UiFilterField>(entity =>
        {
            entity.ToTable("UiFilterField");

            entity.HasIndex(e => new { e.FilterId, e.Key }, "UQ_UiFilterField_Filter_Key").IsUnique();

            entity.Property(e => e.ColClass).HasMaxLength(50);
            entity.Property(e => e.DateFormat).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Label)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.OptionLabel).HasMaxLength(50);
            entity.Property(e => e.OptionValue).HasMaxLength(50);
            entity.Property(e => e.OptionsSource).HasMaxLength(100);
            entity.Property(e => e.Placeholder).HasMaxLength(200);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(d => d.Filter).WithMany(p => p.UisFiltersFields)
                .HasForeignKey(d => d.FilterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UiFilterField_Filter");
        });

        modelBuilder.Entity<UiOptionItem>(entity =>
        {
            entity.ToTable("UiOptionItem");

            entity.HasIndex(e => new { e.ListId, e.Order }, "IX_UiOptionItem_ListId_Order");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Label)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.List).WithMany(p => p.UisOptionsItems)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UiOptionItem_List");
        });

        modelBuilder.Entity<UiOptionList>(entity =>
        {
            entity.ToTable("UiOptionList");

            entity.HasIndex(e => e.Code, "UQ_UiOptionList_Code").IsUnique();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
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
