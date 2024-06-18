using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace floratech.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<favoritosFruta> favoritosFrutas { get; set; }
        public virtual DbSet<favoritosPlaga> favoritosPlagas { get; set; }
        public virtual DbSet<favoritosPlanta> favoritosPlantas { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.nombre_fruta)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.genero)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.especie)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.carbohidratos)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.proteina)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.grasa)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritosFruta>()
                .Property(e => e.azucar)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritosPlaga>()
                .Property(e => e.nombre_plaga)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosPlaga>()
                .Property(e => e.tratamiento)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosPlanta>()
                .Property(e => e.nombre_planta)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosPlanta>()
                .Property(e => e.especie)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosPlanta>()
                .Property(e => e.genero)
                .IsUnicode(false);

            modelBuilder.Entity<favoritosPlanta>()
                .Property(e => e.riego)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.usuario1)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.favoritosFrutas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.favoritosPlagas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.favoritosPlantas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
