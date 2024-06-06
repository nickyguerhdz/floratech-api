namespace floratech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("favoritos")]
    public partial class favoritoFrutas
    {
        public int id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(10)]
        public string tipo { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_fruta { get; set; }

        [Required]
        [StringLength(100)]
        public string genero { get; set; }

        [Required]
        [StringLength(100)]
        public string especie { get; set; }

        public int calorias { get; set; }

        public decimal carbohidratos { get; set; }

        public decimal proteina { get; set; }

        public decimal grasa { get; set; }

        public decimal azucar { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
