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
        public int id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(10)]
        public string tipo { get; set; }

        public int? id_elementoAPI { get; set; }

        [Required]
        [StringLength(100)]
        public string elemento { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
