namespace AccesoBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("escuela")]
    public partial class escuela
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public escuela()
        {
            clientes = new HashSet<cliente>();
        }

        [Key]
        public int id_escuela { get; set; }

        [Required]
        [StringLength(30)]
        public string nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string direccion { get; set; }

        public int usuario_id { get; set; }

        [StringLength(50)]
        public string estatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cliente> clientes { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
