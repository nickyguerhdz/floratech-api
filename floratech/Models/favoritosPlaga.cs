namespace floratech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("favoritosPlaga")]
    public partial class favoritosPlaga
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_plaga { get; set; }

        [Required]
        [StringLength(100)]
        public string tratamiento { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
