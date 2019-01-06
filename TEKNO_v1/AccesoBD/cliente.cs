namespace AccesoBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cliente")]
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            ventas = new HashSet<venta>();
        }

        [Key]
        public int id_cliente { get; set; }

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
        [StringLength(30)]
        public string correoelectronico { get; set; }

        [Required]
        [StringLength(30)]
        public string telefono { get; set; }

        public int escuela_id { get; set; }

        public int usuario_id { get; set; }

        [StringLength(50)]
        public string estatus { get; set; }

        public virtual escuela escuela { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venta> ventas { get; set; }
    }
}
