namespace Acceso_BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class detalle_venta
    {
        [Key]
        public int id_detallevta { get; set; }

        public int producto_id { get; set; }

        public int cantidad { get; set; }

        public int venta_id { get; set; }

        public decimal subtotal { get; set; }

        public virtual producto producto { get; set; }

        public virtual venta venta { get; set; }
    }
}
