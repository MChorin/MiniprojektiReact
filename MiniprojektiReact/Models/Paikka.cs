namespace MiniprojektiReact.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paikka")]
    public partial class Paikka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paikka()
        {
            Kommentti = new HashSet<Kommentti>();
        }

        [Key]
        public int Paikka_id { get; set; }

        public int Kayttaja_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nimi { get; set; }

        [StringLength(500)]
        public string Kuvaus { get; set; }

        [Column(TypeName = "image")]
        public byte[] Paakuva { get; set; }

        [Required]
        [StringLength(50)]
        public string Kategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Katuosoite { get; set; }

        [Required]
        [StringLength(30)]
        public string Kaupunki { get; set; }

        [Required]
        [StringLength(30)]
        public string Maa { get; set; }

        public int KommenttienMaara { get; set; }

        public int ArvostelujenSumma { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kommentti> Kommentti { get; set; }

     /*   public virtual Paikka Paikka1 { get; set; }

        public virtual Paikka Paikka2 { get; set; }*/
    }
}
