using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace floratech.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<favoritoFrutas> favoritos { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.nombre_fruta)
                .IsUnicode(false);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.genero)
                .IsUnicode(false);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.especie)
                .IsUnicode(false);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.carbohidratos)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.proteina)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.grasa)
                .HasPrecision(5, 2);

            modelBuilder.Entity<favoritoFrutas>()
                .Property(e => e.azucar)
                .HasPrecision(5, 2);

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
                .HasMany(e => e.favoritos)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
