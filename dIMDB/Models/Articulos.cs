namespace dIMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articulos
    {
        [Key]
        public int IDArticulo { get; set; }

        [Required]
        [StringLength(1024)]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; }
    }
}
