namespace Acceso_BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class detalle_compra
    {
        [Key]
        public int id_detallecpra { get; set; }

        public int producto_id { get; set; }

        public int cantidad { get; set; }

        public int compra_id { get; set; }

        public decimal subtotal { get; set; }

        public virtual compra compra { get; set; }

        public virtual producto producto { get; set; }
    }
}
