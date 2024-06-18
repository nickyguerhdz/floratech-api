namespace floratech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class favoritosPlanta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_planta { get; set; }

        [Required]
        [StringLength(100)]
        public string especie { get; set; }

        [Required]
        [StringLength(100)]
        public string genero { get; set; }

        [Required]
        [StringLength(100)]
        public string riego { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
