namespace floratech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("favoritos")]
    public partial class favorito
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int usuario_id { get; set; }

        [Required]
        [StringLength(100)]
        public string especie { get; set; }

        [Required]
        [StringLength(100)]
        public string genero { get; set; }

        [Required]
        public decimal carbo { get; set; }

        [Required]
        public decimal proteina { get; set; }

        [Required]
        public decimal grasa { get; set; }

        [Required]
        public decimal azucar { get; set; }

        [Required]
        public virtual usuario usuario { get; set; }
    }
}
