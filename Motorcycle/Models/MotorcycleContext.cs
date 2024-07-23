using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Motorcycle.Models
{
    public partial class MotorcycleContext : DbContext
    {
        public MotorcycleContext()
        {
        }

        public MotorcycleContext(DbContextOptions<MotorcycleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Correocliente> Correoclientes { get; set; } = null!;
        public virtual DbSet<Correousuario> Correousuarios { get; set; } = null!;
        public virtual DbSet<Detalleventa> Detalleventas { get; set; } = null!;
        public virtual DbSet<Estadoventum> Estadoventa { get; set; } = null!;
        public virtual DbSet<Fichatecnica> Fichatecnicas { get; set; } = null!;
        public virtual DbSet<Inventario> Inventarios { get; set; } = null!;
        public virtual DbSet<Movilcliente> Movilclientes { get; set; } = null!;
        public virtual DbSet<Movilusuario> Movilusuarios { get; set; } = null!;
        public virtual DbSet<Ordentrabajo> Ordentrabajos { get; set; } = null!;
        public virtual DbSet<OrdentrabajoServicioHasProducto> OrdentrabajoServicioHasProductos { get; set; } = null!;
        public virtual DbSet<Ordenventum> Ordenventa { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<ServicioHasOrdentrabajo> ServicioHasOrdentrabajos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
 //               optionsBuilder.UseSqlServer("Server=LAPTOP-ACC84CPV\\SQLEXPRESS;Database=Motorcycle;Integrated Security=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("PK__cita__814F31262FC64737");

                entity.ToTable("cita");

                entity.Property(e => e.IdCita).HasColumnName("idCita");

                entity.Property(e => e.FechaCita)
                    .HasColumnType("date")
                    .HasColumnName("fechaCita");

                entity.Property(e => e.FechaFinalizacionCita)
                    .HasColumnType("date")
                    .HasColumnName("fechaFinalizacionCita");

                entity.Property(e => e.HoraCita).HasColumnName("horaCita");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdFichaTecnica).HasColumnName("idFichaTecnica");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cita_cliente");

                entity.HasOne(d => d.IdFichaTecnicaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdFichaTecnica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cita_fichatecnica");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cita_usuario");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__cliente__885457EEA0101FB5");

                entity.ToTable("cliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.ApellidoCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("apellidoCliente");

                entity.Property(e => e.DocumentoCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("documentoCliente");

                entity.Property(e => e.EstadoCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("estadoCliente");

                entity.Property(e => e.GeneroCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("generoCliente");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreCliente");

                entity.Property(e => e.PassWordCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("passWordCliente");

                entity.Property(e => e.RegistroCliente)
                    .HasColumnType("date")
                    .HasColumnName("registroCliente");
            });

            modelBuilder.Entity<Correocliente>(entity =>
            {
                entity.HasKey(e => e.IdCorreoCliente)
                    .HasName("PK__correocl__63A67CACAF250BD2");

                entity.ToTable("correocliente");

                entity.Property(e => e.IdCorreoCliente).HasColumnName("idCorreoCliente");

                entity.Property(e => e.CorreoCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correoCliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Correoclientes)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correoCliente_idCliente");
            });

            modelBuilder.Entity<Correousuario>(entity =>
            {
                entity.HasKey(e => e.IdCorreoUsuario)
                    .HasName("PK__correous__4CE174E0B0743377");

                entity.ToTable("correousuario");

                entity.Property(e => e.IdCorreoUsuario).HasColumnName("idCorreoUsuario");

                entity.Property(e => e.CorreoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correoUsuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Correousuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correousuario_usuario");
            });

            modelBuilder.Entity<Detalleventa>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVentas)
                    .HasName("PK__detallev__FF899D7D61F04BEB");

                entity.ToTable("detalleventas");

                entity.Property(e => e.IdDetalleVentas).HasColumnName("idDetalleVentas");

                entity.Property(e => e.CantidaDetalleVentas).HasColumnName("cantidaDetalleVentas");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.ValorTotalDetalleVentas)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valorTotalDetalleVentas");

                entity.Property(e => e.ValorUnitarioDetalleVentas)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valorUnitarioDetalleVentas");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalleventas_producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalleventas_ordenventa");
            });

            modelBuilder.Entity<Estadoventum>(entity =>
            {
                entity.HasKey(e => e.IdEstadoVenta)
                    .HasName("PK__estadove__0B15BAB50CD59AD8");

                entity.ToTable("estadoventa");

                entity.Property(e => e.IdEstadoVenta).HasColumnName("idEstadoVenta");

                entity.Property(e => e.NombreEstadoVenta)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreEstadoVenta");
            });

            modelBuilder.Entity<Fichatecnica>(entity =>
            {
                entity.HasKey(e => e.IdFichaTecnica)
                    .HasName("PK__fichatec__4AE1FB77532C8661");

                entity.ToTable("fichatecnica");

                entity.Property(e => e.IdFichaTecnica).HasColumnName("idFichaTecnica");

                entity.Property(e => e.ChacisFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("chacisFichaTecnica");

                entity.Property(e => e.CilindrajeFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cilindrajeFichaTecnica");

                entity.Property(e => e.FechaRegistroFichaTecnica)
                    .HasColumnType("date")
                    .HasColumnName("fechaRegistroFichaTecnica");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.MarcaFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("marcaFichaTecnica");

                entity.Property(e => e.ModeloFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("modeloFichaTecnica");

                entity.Property(e => e.NumeroMotorFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("numeroMotorFichaTecnica");

                entity.Property(e => e.PlacaFichaTecnica)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("placaFichaTecnica");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Fichatecnicas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fichaTecnica_idCliente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Fichatecnicas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fichatecnica_usuario1");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.IdInventario)
                    .HasName("PK__inventar__8F145B0D12681AC5");

                entity.ToTable("inventario");

                entity.Property(e => e.IdInventario).HasColumnName("idInventario");

                entity.Property(e => e.CantidadInventario).HasColumnName("cantidadInventario");

                entity.Property(e => e.FechaMovimientoInventario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMovimientoInventario");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.ValorUnitarioInventario)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valorUnitarioInventario");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inventario_producto");
            });

            modelBuilder.Entity<Movilcliente>(entity =>
            {
                entity.HasKey(e => e.IdMovilCliente)
                    .HasName("PK__movilcli__35C78DA27E168FD5");

                entity.ToTable("movilcliente");

                entity.Property(e => e.IdMovilCliente).HasColumnName("idMovilCliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.NumeroMovilCliente)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("numeroMovilCliente");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Movilclientes)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movilCliente_idCliente");
            });

            modelBuilder.Entity<Movilusuario>(entity =>
            {
                entity.HasKey(e => e.IdMovilUsuario)
                    .HasName("PK__movilusu__C40BD4E76A687240");

                entity.ToTable("movilusuario");

                entity.Property(e => e.IdMovilUsuario).HasColumnName("idMovilUsuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NumeroMovilUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("numeroMovilUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Movilusuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movilusuario_usuario");
            });

            modelBuilder.Entity<Ordentrabajo>(entity =>
            {
                entity.HasKey(e => e.IdOrdenTrabajo)
                    .HasName("PK__ordentra__7C7B3DBB69667420");

                entity.ToTable("ordentrabajo");

                entity.Property(e => e.IdOrdenTrabajo).HasColumnName("idOrdenTrabajo");

                entity.Property(e => e.IdCita).HasColumnName("idCita");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.NumeroOrdenTrabajo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("numeroOrdenTrabajo");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.Ordentrabajos)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordentrabajo_cita");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Ordentrabajos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordentrabajo_ordenventa");
            });

            modelBuilder.Entity<OrdentrabajoServicioHasProducto>(entity =>
            {
                entity.HasKey(e => e.IdOrdentrabajoServicioHasProductos)
                    .HasName("PK__ordentra__9B7B1CB234343EB4");

                entity.ToTable("ordentrabajo_servicio_has_productos");

                entity.Property(e => e.IdOrdentrabajoServicioHasProductos).HasColumnName("idOrdentrabajo_Servicio_has_Productos");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.IdServicioOrdenTrabajo).HasColumnName("idServicio_OrdenTrabajo");

                entity.Property(e => e.PrecioProductoOrdentrabajoServicioHasProductos)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precioProducto_Ordentrabajo_Servicio_has_Productos");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.OrdentrabajoServicioHasProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ordentrab__idPro__619B8048");

                entity.HasOne(d => d.IdServicioOrdenTrabajoNavigation)
                    .WithMany(p => p.OrdentrabajoServicioHasProductos)
                    .HasForeignKey(d => d.IdServicioOrdenTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordentrabajo_servicio_has_productos_servicio_has_ordentrabajo");
            });

            modelBuilder.Entity<Ordenventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__ordenven__077D5614D4A50463");

                entity.ToTable("ordenventa");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.FechaOrdenVenta)
                    .HasColumnType("date")
                    .HasColumnName("fechaOrdenVenta");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdEstadoVenta).HasColumnName("idEstadoVenta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.ValorTotalVenta)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valorTotalVenta");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Ordenventa)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordenventa_cliente");

                entity.HasOne(d => d.IdEstadoVentaNavigation)
                    .WithMany(p => p.Ordenventa)
                    .HasForeignKey(d => d.IdEstadoVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordenventa_estadoventa");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Ordenventa)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ordenventa_usuario");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__producto__07F4A1324A186C74");

                entity.ToTable("producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("codigoProducto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("descripcionProducto");

                entity.Property(e => e.FotoProducto).HasColumnName("fotoProducto");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreProducto");

                entity.Property(e => e.ValorUnitarioProducto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valorUnitarioProducto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto__idUsua__571DF1D5");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__rol__3C872F765211E336");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreRol");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("PK__servicio__CEB981192572CB90");

                entity.ToTable("servicio");

                entity.Property(e => e.IdServicio).HasColumnName("idServicio");

                entity.Property(e => e.NombreServicio)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreServicio");

                entity.Property(e => e.PrecioManoObraServicio)
                    .HasColumnType("decimal(10, 0)")
                    .HasColumnName("precioManoObraServicio");
            });

            modelBuilder.Entity<ServicioHasOrdentrabajo>(entity =>
            {
                entity.HasKey(e => e.IdServicioOrdenTrabajo)
                    .HasName("PK__servicio__23B5E827FA170C21");

                entity.ToTable("servicio_has_ordentrabajo");

                entity.Property(e => e.IdServicioOrdenTrabajo).HasColumnName("idServicio_OrdenTrabajo");

                entity.Property(e => e.IdOrdenTrabajo).HasColumnName("idOrdenTrabajo");

                entity.Property(e => e.IdServicio).HasColumnName("idServicio");

                entity.Property(e => e.PrecioServicioHasOrdenTrabajo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precioServicio_has_OrdenTrabajo");

                entity.HasOne(d => d.IdOrdenTrabajoNavigation)
                    .WithMany(p => p.ServicioHasOrdentrabajos)
                    .HasForeignKey(d => d.IdOrdenTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__servicio___idOrd__656C112C");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.ServicioHasOrdentrabajos)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__servicio___idSer__6477ECF3");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__645723A67D20520F");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.ApellidoUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("apellidoUsuario");

                entity.Property(e => e.DocumentoUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("documentoUsuario");

                entity.Property(e => e.EstadoUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("estadoUsuario");

                entity.Property(e => e.GeneroUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("generoUsuario");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombreUsuario");

                entity.Property(e => e.PasswordUsuario)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("passwordUsuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
