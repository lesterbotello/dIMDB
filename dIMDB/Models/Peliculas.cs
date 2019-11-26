namespace dIMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Peliculas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Peliculas()
        {
            ActoresPeliculas = new HashSet<ActoresPeliculas>();
            GuionistasPeliculas = new HashSet<GuionistasPeliculas>();
        }

        [Key]
        [Display(Name = "Código")]
        public int IDPelicula { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Sinopsis")]
        public string Sinopsis { get; set; }

        [Display(Name = "Director")]
        public int IDDirector { get; set; }

        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Carátula")]
        public string RutaCaratula { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Trailer")]
        public string RutaTrailer { get; set; }

        [Display(Name = "Género")]
        public int IDGenero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActoresPeliculas> ActoresPeliculas { get; set; }

        public virtual Directores Directores { get; set; }

        public virtual Generos Generos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuionistasPeliculas> GuionistasPeliculas { get; set; }
    }
}
