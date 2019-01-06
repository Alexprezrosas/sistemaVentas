namespace AccesoBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public compra()
        {
            detalle_compra = new HashSet<detalle_compra>();
        }

        [Key]
        public int id_compra { get; set; }

        public DateTime fecha_hora { get; set; }

        public int proveedor_id { get; set; }

        public int usuario_id { get; set; }

        public decimal total { get; set; }

        public virtual proveedor proveedor { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_compra> detalle_compra { get; set; }
    }
}
