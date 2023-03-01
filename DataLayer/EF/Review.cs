namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int ID { get; set; }

        public int? ProductsID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int? Status { get; set; }

        public DateTime? Create_at { get; set; }

        public DateTime? Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
    }
}
