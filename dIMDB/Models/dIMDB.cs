namespace dIMDB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dIMDBEntities : DbContext
    {
        public dIMDBEntities()
            : base("name=dIMDBEntities")
        {
        }

        public virtual DbSet<Actores> Actores { get; set; }
        public virtual DbSet<ActoresPeliculas> ActoresPeliculas { get; set; }
        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Directores> Directores { get; set; }
        public virtual DbSet<Generos> Generos { get; set; }
        public virtual DbSet<Gionistas> Gionistas { get; set; }
        public virtual DbSet<GuionistasPeliculas> GuionistasPeliculas { get; set; }
        public virtual DbSet<Peliculas> Peliculas { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actores>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Actores>()
                .Property(e => e.LugarNacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Actores>()
                .HasMany(e => e.ActoresPeliculas)
                .WithRequired(e => e.Actores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Articulos>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Articulos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Articulos>()
                .Property(e => e.CreadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.ActionName)
                .IsUnicode(false);

            modelBuilder.Entity<Directores>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Directores>()
                .Property(e => e.LugarNacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Directores>()
                .HasMany(e => e.Peliculas)
                .WithRequired(e => e.Directores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Generos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Generos>()
                .HasMany(e => e.Peliculas)
                .WithRequired(e => e.Generos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gionistas>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Gionistas>()
                .Property(e => e.LugarNacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Gionistas>()
                .HasMany(e => e.GuionistasPeliculas)
                .WithRequired(e => e.Gionistas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.Sinopsis)
                .IsUnicode(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.RutaCaratula)
                .IsUnicode(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.RutaTrailer)
                .IsUnicode(false);

            modelBuilder.Entity<Peliculas>()
                .HasMany(e => e.ActoresPeliculas)
                .WithRequired(e => e.Peliculas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peliculas>()
                .HasMany(e => e.GuionistasPeliculas)
                .WithRequired(e => e.Peliculas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
