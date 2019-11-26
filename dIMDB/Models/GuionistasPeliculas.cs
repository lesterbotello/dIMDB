namespace dIMDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GuionistasPeliculas
    {
        [Key]
        public int IDGuionistaPelicula { get; set; }

        public int IDGuionista { get; set; }

        public int IDPelicula { get; set; }

        public virtual Gionistas Gionistas { get; set; }

        public virtual Peliculas Peliculas { get; set; }
    }
}
