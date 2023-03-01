namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WishList")]
    public partial class WishList
    {
        public int ID { get; set; }

        public int? ProductsID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
    }
}
