namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Decription { get; set; }

        [Column(TypeName = "ntext")]
        public string Information { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}
