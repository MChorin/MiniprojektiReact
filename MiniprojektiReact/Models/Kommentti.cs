namespace MiniprojektiReact.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kommentti")]
    public partial class Kommentti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kommentti()
        {
            Kuvat1 = new HashSet<Kuvat>();
        }

        [Key]
        public int Kommentti_id { get; set; }

        public int Kayttaja_id { get; set; }

        public int Paikka_id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Kuvat { get; set; }

        [StringLength(1000)]
        public string Teksti { get; set; }

        public DateTime Aikaleima { get; set; }

        public bool? OnkoKuva { get; set; }

        public int Arvosana { get; set; }

        public virtual Paikka Paikka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kuvat> Kuvat1 { get; set; }
    }
}
