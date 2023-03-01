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
        public string Path { get; set; }

        public DateTime? Create_at { get; set; }

        public DateTime? Update_at { get; set; }
    }
}
