namespace MiniprojektiReact.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MPdbModel : DbContext
    {
        public MPdbModel()
            : base("name=MPdbModel")
        {
        }

        public virtual DbSet<Kommentti> Kommentti { get; set; }
        public virtual DbSet<Kuvat> Kuvat { get; set; }
        public virtual DbSet<Paikka> Paikka { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kommentti>()
                .Property(e => e.Teksti)
                .IsFixedLength();

            modelBuilder.Entity<Kommentti>()
                .HasMany(e => e.Kuvat1)
                .WithRequired(e => e.Kommentti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Nimi)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Kuvaus)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Kategoria)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Katuosoite)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Kaupunki)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .Property(e => e.Maa)
                .IsFixedLength();

            modelBuilder.Entity<Paikka>()
                .HasMany(e => e.Kommentti)
                .WithRequired(e => e.Paikka)
                .WillCascadeOnDelete(false);

          /*  modelBuilder.Entity<Paikka>()
                .HasOptional(e => e.Paikka1)
                .WithRequired(e => e.Paikka2);*/
        }
    }
}
