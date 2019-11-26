namespace dIMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    public partial class Actores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actores()
        {
            ActoresPeliculas = new HashSet<ActoresPeliculas>();
        }

        [Key]
        [Display(Name = "Código")]
        public int IDActor { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre Actor")]
        public string Nombre { get; set; }

        [StringLength(100)]
        [Display(Name = "Lugar de Nacimiento")]
        public string LugarNacimiento { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActoresPeliculas> ActoresPeliculas { get; set; }
    }
}
