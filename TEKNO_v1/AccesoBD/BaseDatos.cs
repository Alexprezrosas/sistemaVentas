namespace AccesoBD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BaseDatos : DbContext
    {
        public BaseDatos()
            : base("name=BaseDatos")
        {
        }

        public virtual DbSet<cliente> clientes { get; set; }
        public virtual DbSet<compra> compras { get; set; }
        public virtual DbSet<detalle_compra> detalle_compra { get; set; }
        public virtual DbSet<detalle_venta> detalle_venta { get; set; }
        public virtual DbSet<escuela> escuelas { get; set; }
        public virtual DbSet<producto> productoes { get; set; }
        public virtual DbSet<proveedor> proveedors { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<venta> ventas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cliente>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.ap_paterno)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.ap_materno)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.correoelectronico)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.estatus)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.ventas)
                .WithRequired(e => e.cliente)
                .HasForeignKey(e => e.cliente_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<compra>()
                .Property(e => e.total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<compra>()
                .HasMany(e => e.detalle_compra)
                .WithRequired(e => e.compra)
                .HasForeignKey(e => e.compra_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<detalle_compra>()
                .Property(e => e.subtotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<detalle_venta>()
                .Property(e => e.subtotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<detalle_venta>()
                .Property(e => e.precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<escuela>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<escuela>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<escuela>()
                .Property(e => e.estatus)
                .IsUnicode(false);

            modelBuilder.Entity<escuela>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.escuela)
                .HasForeignKey(e => e.escuela_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.marca)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.almacen)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.preciocompra)
                .HasPrecision(10, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.precioprofesor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.preciopublico)
                .HasPrecision(10, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.estatus)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .HasMany(e => e.detalle_compra)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .HasMany(e => e.detalle_venta)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.ap_paterno)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.ap_materno)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.RFC)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<proveedor>()
                .HasMany(e => e.compras)
                .WithRequired(e => e.proveedor)
                .HasForeignKey(e => e.proveedor_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.ap_paterno)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.ap_materno)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.puesto)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.compras)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.escuelas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.productoes)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.proveedors)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.ventas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<venta>()
                .Property(e => e.total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<venta>()
                .HasMany(e => e.detalle_venta)
                .WithRequired(e => e.venta)
                .HasForeignKey(e => e.venta_id)
                .WillCascadeOnDelete(false);
        }
    }
}
