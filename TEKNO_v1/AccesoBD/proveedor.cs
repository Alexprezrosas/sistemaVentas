namespace AccesoBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("proveedor")]
    public partial class proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedor()
        {
            compras = new HashSet<compra>();
        }

        [Key]
        public int id_proveedor { get; set; }

        [Required]
        [StringLength(30)]
        public string nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string ap_paterno { get; set; }

        [Required]
        [StringLength(30)]
        public string ap_materno { get; set; }

        [Required]
        [StringLength(18)]
        public string RFC { get; set; }

        [Required]
        [StringLength(30)]
        public string direccion { get; set; }

        public int usuario_id { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compras { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
