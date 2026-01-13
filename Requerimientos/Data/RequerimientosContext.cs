using Microsoft.EntityFrameworkCore;
using Requerimientos.Models;

namespace Requerimientos.Data;

public partial class RequerimientosContext : DbContext
{
    public RequerimientosContext()
    {
    }

    public RequerimientosContext(DbContextOptions<RequerimientosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Requerimiento> Requerimientos { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Proveedor> Proveedores { get; set; }
    public virtual DbSet<Compra> Compras { get; set; }
    public virtual DbSet<Trabajador> Trabajadores { get; set; }
    public virtual DbSet<Clase> Clases { get; set; }
    public virtual DbSet<Vehiculo> Vehiculos { get; set; }
    public virtual DbSet<Pertenece> Perteneces { get; set; }
    public virtual DbSet<Movimiento> Movimientos { get; set; }
    public virtual DbSet<Combustible> Combustibles { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Producto> Productos{ get; set; }
    public virtual DbSet<Unidad> Unidades{ get; set; }
    public virtual DbSet<Kardex> Kardex { get; set; }
    public virtual DbSet<EntregaMaterial> EntregaMateriales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Requerimiento>(entity =>
        {
            entity.ToTable("Requerimiento");

            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Detalle).IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Numero)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Solicitante)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Unidad)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Rol");

            entity.Property(e => e.Descripción)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("Proveedor");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);

            entity.Property(e => e.RUC)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.ToTable("Compra");

            entity.Property(e => e.Fecha)
                .IsRequired(true)
                .HasColumnType("date");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false) 
                .IsRequired(true);

            entity.Property(e => e.Cantidad)
                .HasColumnType("int")
                .IsRequired(false);

            entity.Property(e => e.Unidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);

            entity.Property(e => e.CondicionPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            entity.Property(e => e.Monto)
                .HasColumnType("money")
                .IsRequired(false);

            entity.Property(e => e.NroDocumento)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(e => e.Evidencia)
                .HasMaxLength(250)
                .IsRequired(false);

            entity.Property(e => e.Comprador)
                .HasMaxLength(150)
                .IsRequired(false);

            entity.Property(e => e.Ingreso)
                .HasColumnType("money")
                .IsRequired(false);

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compra_Proveedor");

        });

        modelBuilder.Entity<Trabajador>(entity =>
        {
            entity.ToTable("Trabajador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.DNI)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.Clase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.NroBBVA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.CCIBBVA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.NroBCP)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.CCIBCP)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.NroInterbank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.CCIInterbank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.ToTable("Clase");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculo");
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(true);
            entity.HasIndex(e => e.Placa).IsUnique();
            entity.HasOne(d => d.Clase)
                .WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.ClaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_Clase");
        });

        modelBuilder.Entity<Pertenece>(entity =>
        {
            entity.ToTable("Pertenece");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("Movimiento");

            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .IsRequired(true);

            entity.Property(e => e.HoraIngreso)
                .HasColumnType("time")
                .IsRequired(false);

            entity.Property(e => e.HoraSalida)
                .HasColumnType("time")
                .IsRequired(false);

            entity.Property(e => e.Motivo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsRequired(false);

            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsRequired(false);

            entity.Property(e => e.HorometroInicio).HasColumnType("decimal(10,2)");
            entity.Property(e => e.HorometroFinal).HasColumnType("decimal(10,2)");
            entity.Property(e => e.HorometroTotal).HasColumnType("decimal(10,2)");
            entity.Property(e => e.HorometroInicio2).HasColumnType("decimal(10,2)");
            entity.Property(e => e.HorometroFinal2).HasColumnType("decimal(10,2)");
            entity.Property(e => e.HorometroTotal2).HasColumnType("decimal(10,2)");

            entity.Property(e => e.TotalM3Desm)
                .HasColumnType("int");

            entity.Property(e => e.VueltasVolq)
                .HasColumnType("int")
                .IsRequired(false);

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Vehiculo)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimiento_Vehiculo");

            entity.HasOne(d => d.Chofer)
                .WithMany()
                .HasForeignKey(d => d.ChoferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimiento_Chofer_Trabajador");

            entity.HasOne(d => d.Pertenece)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.PerteneceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimiento_Pertenece");

            entity.HasOne(d => d.Proveedor)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimiento_Proveedor");
        });

        modelBuilder.Entity<Combustible>(entity =>
        {
            entity.ToTable("Combustible");

            entity.Property(e => e.Fecha).HasColumnType("date").IsRequired(true);
            entity.Property(e => e.HoraEntrega).HasColumnType("time").IsRequired(false);

            entity.Property(e => e.GlnsEntregado).HasColumnType("decimal(10,2)").IsRequired(false);
            entity.Property(e => e.Litros).HasColumnType("decimal(10,2)").IsRequired(false);

            entity.Property(e => e.GlnsXLitro).HasColumnType("decimal(10,6)").IsRequired(true);

            entity.Property(e => e.TotalGalones).HasColumnType("decimal(10,2)");
            entity.Property(e => e.GalonesRecibidos).HasColumnType("decimal(10,2)");

            entity.Property(e => e.CostoXGalon).HasColumnType("money").IsRequired(true);
            entity.Property(e => e.PrecioTotal).HasColumnType("money");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Clase)
                .WithMany()
                .HasForeignKey(d => d.ClaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Combustible_Clase");

            entity.HasOne(d => d.Responsable)
                .WithMany()
                .HasForeignKey(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Combustible_Responsable_Trabajador");

            entity.HasOne(d => d.QuienRecibe)
                .WithMany()
                .HasForeignKey(d => d.QuienRecibeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Combustible_QuienRecibe_Trabajador");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.Prefijo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.HasIndex(e => e.Prefijo)
                .IsUnique();
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Producto");
            entity.Property(e => e.Codigo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.StockMinimo)
                .HasColumnType("decimal(10,2)")
                .IsRequired(true);
            entity.Property(e => e.Proviene)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.Condicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.NroRequerimiento)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.AreaRequerimiento)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);
            entity.HasIndex(e => e.Codigo)
                .IsUnique();
            entity.HasOne(d => d.Unidad)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.UnidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Unidad");
            entity.HasOne(d => d.Categoria)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");
            entity.HasOne(d => d.Pertenece)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.PerteneceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Pertenece");
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.ToTable("Unidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.Simbolo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(true);
            entity.HasIndex(e => e.Simbolo)
                .IsUnique();
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.ToTable("Kardex");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .IsRequired(true);
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18,2)")
                .IsRequired(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsRequired(true);
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .IsRequired(false);
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("money")
                .IsRequired(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Producto)
                .WithMany(p => p.Kardex)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_Producto");
            entity.HasOne(d => d.QuienRecibe)
                .WithMany(d => d.KardexRecibidos)
                .HasForeignKey(d => d.QuienRecibeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_QuienRecibe_Trabajador");
            entity.HasOne(d => d.QuienEntrega)
                .WithMany(d => d.KardexEntregados)
                .HasForeignKey(d => d.QuienEntregaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_QuienEntrega_Trabajador");
        });

        modelBuilder.Entity<EntregaMaterial>(entity =>
        {
            entity.ToTable("EntregaMaterial");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .IsRequired(true);
            entity.Property(e => e.CantidadEntregada)
                .HasColumnType("decimal(18,2)")
                .IsRequired(true);
            entity.Property(e => e.Material)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.Retorno)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired(false);
            entity.Property(e => e.HoraSalida)
                .HasColumnType("time")
                .IsRequired(false);
            entity.Property(e => e.HoraIngreso)
                .HasColumnType("time")
                .IsRequired(false);
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("date")
                .IsRequired(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Producto)
                .WithMany(p => p.EntregaMateriales)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntregaMaterial_Producto");
            entity.HasOne(d => d.AQuienSeEntrega)
                .WithMany(d => d.EntregasRecibidas)
                .HasForeignKey(d => d.AQuienSeEntregaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntregaMaterial_AQuienSeEntrega_Trabajador");
            entity.HasOne(d => d.Responsable)
                .WithMany(d => d.EntregasResponsable)
                .HasForeignKey(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntregaMaterial_Responsable_Trabajador");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
