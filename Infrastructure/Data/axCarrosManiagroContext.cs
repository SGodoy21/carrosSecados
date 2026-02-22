using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class axCarrosManiagroContext : DbContext
{
    public axCarrosManiagroContext()
    {
    }

    public axCarrosManiagroContext(DbContextOptions<axCarrosManiagroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BocaSecadora> BocasSecadoras { get; set; }
    public virtual DbSet<Carro> Carros { get; set; }
    public virtual DbSet<Estado> Estados { get; set; }
    public virtual DbSet<HistorialEstado> HistorialsEstados { get; set; }
    public virtual DbSet<ParametroConfiguracion> ParametrosConfiguracions { get; set; }
    public virtual DbSet<Rol> Roles { get; set; }
    public virtual DbSet<Secadora> Secadoras { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<CrudConfig> CrudsConfigs { get; set; }
    public virtual DbSet<CrudConfigAccion> CrudsConfigsAccions { get; set; }
    public virtual DbSet<CrudConfigCampo> CrudsConfigsCampos { get; set; }
    public virtual DbSet<CrudConfigEncabezado> CrudsConfigsEncabezados { get; set; }
    public virtual DbSet<Filtro> Filtros { get; set; }
    public virtual DbSet<FiltroCampo> FiltrosCampos { get; set; }
    public virtual DbSet<FiltroOptionSource> FiltrosOptionsSources { get; set; }
    public virtual DbSet<MenuItem> MenusItems { get; set; }
    public virtual DbSet<UiFilterConfig> UisFiltersConfigs { get; set; }
    public virtual DbSet<UiFilterField> UisFiltersFields { get; set; }
    public virtual DbSet<UiOptionItem> UisOptionsItems { get; set; }
    public virtual DbSet<UiOptionList> UisOptionsLists { get; set; }
    public virtual DbSet<MenuItemRol> MenusItemsRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:axCarrosSecadoresConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BocaSecadora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__boca_sec__3213E83F3E35FC48");

            entity.ToTable("boca_secadora");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdSecadora).HasColumnName("id_secadora");
            entity.Property(e => e.Posicion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("posicion");

            entity.HasOne(d => d.IdSecadoraNavigation).WithMany(p => p.BocasSecadoras)
                .HasForeignKey(d => d.IdSecadora)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_boca_secadora_secadora");
        });

        modelBuilder.Entity<Carro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carro__3213E83F6F2548D9");

            entity.ToTable("carro");

            entity.HasIndex(e => e.TagNfc, "UQ_carro_tag").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Lote)
                .HasMaxLength(250)
                .HasColumnName("lote");
            entity.Property(e => e.NumeroCarro).HasColumnName("numero_carro");
            entity.Property(e => e.TagNfc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tag_nfc");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estado__3213E83F1EFA715A");

            entity.ToTable("estado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ambito)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ambito");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<HistorialEstado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__historia__3213E83F2BD767BB");

            entity.ToTable("historial_estado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaHoraFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_fin");
            entity.Property(e => e.FechaHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_inicio");
            entity.Property(e => e.IdCarro).HasColumnName("id_carro");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdSecadora).HasColumnName("id_secadora");

            entity.HasOne(d => d.IdCarroNavigation).WithMany(p => p.HistorialsEstados)
                .HasForeignKey(d => d.IdCarro)
                .HasConstraintName("FK_HE_Carro");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.HistorialsEstados)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HE_Estado");

            entity.HasOne(d => d.IdSecadoraNavigation).WithMany(p => p.HistorialsEstados)
                .HasForeignKey(d => d.IdSecadora)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HE_Secadora");
        });

        modelBuilder.Entity<ParametroConfiguracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__parametr__3213E83F99E8585A");

            entity.ToTable("parametros_configuracion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClaveNombre)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clave_nombre");
            entity.Property(e => e.ClaveValor)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("clave_valor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83FF202606D");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Secadora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__secadora__3213E83F11E485E3");

            entity.ToTable("secadora");

            entity.HasIndex(e => e.TagNfc, "UQ_secadora_tag").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContErrores).HasColumnName("cont_errores");
            entity.Property(e => e.Controlador)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("controlador");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Escribiendo).HasColumnName("escribiendo");
            entity.Property(e => e.FechaComSecado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_com_secado");
            entity.Property(e => e.FechaFinSecado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin_secado");
            entity.Property(e => e.FirmwareNuevo).HasColumnName("firmware_nuevo");
            entity.Property(e => e.Habilitado).HasColumnName("habilitado");
            entity.Property(e => e.HabilitarSecado).HasColumnName("habilitar_secado");
            entity.Property(e => e.Histeresis).HasColumnName("histeresis");
            entity.Property(e => e.HsRestante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hs_restante");
            entity.Property(e => e.HsSecar)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hs_secar");
            entity.Property(e => e.HumFinal)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("hum_final");
            entity.Property(e => e.HumInicial)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("hum_inicial");
            entity.Property(e => e.IdNave).HasColumnName("id_nave");
            entity.Property(e => e.IdRed).HasColumnName("id_red");
            entity.Property(e => e.Leyendo).HasColumnName("leyendo");
            entity.Property(e => e.Local).HasColumnName("local");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mensaje");
            entity.Property(e => e.Muestra)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("muestra");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NormalAbiertoQuem).HasColumnName("normal_abierto_quem");
            entity.Property(e => e.NormalAbiertoVent).HasColumnName("normal_abierto_vent");
            entity.Property(e => e.PuntosABajar)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("puntos_a_bajar");
            entity.Property(e => e.PuntosXHoras)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("puntos_x_horas");
            entity.Property(e => e.QuemMarcha).HasColumnName("quem_marcha");
            entity.Property(e => e.Rearranque).HasColumnName("rearranque");
            entity.Property(e => e.Secando).HasColumnName("secando");
            entity.Property(e => e.SpSecado).HasColumnName("sp_secado");
            entity.Property(e => e.TagNfc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tag_nfc");
            entity.Property(e => e.TempActual).HasColumnName("temp_actual");
            entity.Property(e => e.TempAmbiente).HasColumnName("temp_ambiente");
            entity.Property(e => e.TipoSecado).HasColumnName("tipo_secado");

            entity.HasMany(d => d.IdsCarros).WithMany(p => p.IdsSecadoras)
                .UsingEntity<Dictionary<string, object>>(
                    "SecadoraCarro",
                    r => r.HasOne<Carro>().WithMany()
                        .HasForeignKey("IdCarro")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SC_Carro"),
                    l => l.HasOne<Secadora>().WithMany()
                        .HasForeignKey("IdSecadora")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SC_Secadora"),
                    j =>
                    {
                        j.HasKey("IdSecadora", "IdCarro").HasName("PK_Secadora_Carro");
                        j.ToTable("secadora_carro");
                        j.IndexerProperty<long>("IdSecadora").HasColumnName("id_secadora");
                        j.IndexerProperty<long>("IdCarro").HasColumnName("id_carro");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83FD05BC665");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccesosIncorrectos).HasColumnName("accesos_incorrectos");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.Habilitado)
                .HasDefaultValue(true)
                .HasColumnName("habilitado");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("password_hash");
            entity.Property(e => e.RecoveryToken)
                .HasMaxLength(255)
                .HasColumnName("recovery_token");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuario_rol");
        });

        modelBuilder.Entity<CrudConfig>(entity =>
        {
            entity.HasKey(e => e.IdCrudConfig).HasName("PK__CrudConf__C512F5759ACC0D7F");
            entity.ToTable("CrudConfig");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CrearTexto).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.FormRoute).IsRequired().HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.ImagenCampo).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.NombreClave).IsRequired().HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(150).IsUnicode(false);
        });

        modelBuilder.Entity<CrudConfigAccion>(entity =>
        {
            entity.HasKey(e => e.IdAccion).HasName("PK__CrudConf__9845169B6EF329B9");
            entity.ToTable("CrudConfigAccion");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Color).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Endpoint).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.Icono).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false);

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
            entity.Property(e => e.NombreCampo).IsRequired().HasMaxLength(100).IsUnicode(false);

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
            entity.Property(e => e.Texto).IsRequired().HasMaxLength(150).IsUnicode(false);

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
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(80).IsUnicode(false);
        });

        modelBuilder.Entity<FiltroCampo>(entity =>
        {
            entity.HasKey(e => e.IdCampo).HasName("PK__FiltroCa__6C61DA81E076F4D1");
            entity.ToTable("FiltroCampo");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Conditions).HasMaxLength(300).IsUnicode(false);
            entity.Property(e => e.Format).HasMaxLength(40).IsUnicode(false);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.OptionsSourceCode).HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.PlaceHolder).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.Size).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(120).IsUnicode(false);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(30).IsUnicode(false);

            entity.HasOne(d => d.IdFiltroNavigation).WithMany(p => p.FiltrosCampos)
                .HasForeignKey(d => d.IdFiltro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FiltroCampo_Filtro");
        });

        modelBuilder.Entity<FiltroOptionSource>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__FiltroOp__A25C5AA6238B4B4E");
            entity.ToTable("FiltroOptionsSource");
            entity.Property(e => e.Code).HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CacheSeconds).HasDefaultValue(120);
            entity.Property(e => e.SourceType).IsRequired().HasMaxLength(20).IsUnicode(false);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.Label).HasMaxLength(100);
        });

        modelBuilder.Entity<UiFilterConfig>(entity =>
        {
            entity.ToTable("UiFilterConfig");
            entity.HasIndex(e => e.Code, "UQ_UiFilterConfig_Code").IsUnique();
            entity.Property(e => e.ApplyLabel).HasMaxLength(50);
            entity.Property(e => e.ClearLabel).HasMaxLength(50);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(100);
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
            entity.Property(e => e.Key).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Label).IsRequired().HasMaxLength(200);
            entity.Property(e => e.OptionLabel).HasMaxLength(50);
            entity.Property(e => e.OptionValue).HasMaxLength(50);
            entity.Property(e => e.OptionsSource).HasMaxLength(100);
            entity.Property(e => e.Placeholder).HasMaxLength(200);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(30);

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
            entity.Property(e => e.Label).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Value).IsRequired().HasMaxLength(200);

            entity.HasOne(d => d.List).WithMany(p => p.UisOptionsItems)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UiOptionItem_List");
        });

        modelBuilder.Entity<UiOptionList>(entity =>
        {
            entity.ToTable("UiOptionList");
            entity.HasIndex(e => e.Code, "UQ_UiOptionList_Code").IsUnique();
            entity.Property(e => e.Code).IsRequired().HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
        });


        modelBuilder.Entity<MenuItemRol>(entity =>
        {
            entity.HasKey(e => new { e.MenuItemId, e.RolId });
            entity.ToTable("MenuItemRoles");

            // Relación con MenuItem
            entity.HasOne(d => d.MenuItem) 
                .WithMany(p => p.MenuItemRoles)
                .HasForeignKey(d => d.MenuItemId) 
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItemRoles_MenuItems");

            // Relación con Rol
            entity.HasOne(d => d.Rol) 
                .WithMany(p => p.MenuItemRoles)
                .HasForeignKey(d => d.RolId) 
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItemRoles_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
        
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
