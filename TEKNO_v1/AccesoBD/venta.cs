namespace AccesoBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("venta")]
    public partial class venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public venta()
        {
            detalle_venta = new HashSet<detalle_venta>();
        }

        [Key]
        public int id_venta { get; set; }

        public DateTime? fecha_hora { get; set; }

        public int cliente_id { get; set; }

        public int usuario_id { get; set; }

        public decimal total { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_venta> detalle_venta { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
