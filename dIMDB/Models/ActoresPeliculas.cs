namespace dIMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActoresPeliculas
    {
        [Key]
        public int IDActorPelicula { get; set; }

        public int IDActor { get; set; }

        public int IDPelicula { get; set; }

        public virtual Actores Actores { get; set; }

        public virtual Peliculas Peliculas { get; set; }
    }
}
