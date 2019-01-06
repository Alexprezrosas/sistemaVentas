namespace Acceso_BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("producto")]
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            detalle_compra = new HashSet<detalle_compra>();
            detalle_venta = new HashSet<detalle_venta>();
        }

        [Key]
        public int id_producto { get; set; }

        [Required]
        [StringLength(30)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string marca { get; set; }

        [Required]
        [StringLength(20)]
        public string almacen { get; set; }

        public int usuario_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_compra> detalle_compra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_venta> detalle_venta { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
