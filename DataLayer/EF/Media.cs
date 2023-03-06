namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medias")]
    public partial class Media
    {
        public int ID { get; set; }

        public int? ProductsID { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
