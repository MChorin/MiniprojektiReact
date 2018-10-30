namespace MiniprojektiReact.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kuvat")]
    public partial class Kuvat
    {
        [Key]
        public int Kuva_id { get; set; }

        public int Kommentti_id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] Kuva { get; set; }

        public virtual Kommentti Kommentti { get; set; }
    }
}
