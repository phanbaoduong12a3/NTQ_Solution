namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int ID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        [StringLength(50)]
        public string Detail { get; set; }

        public bool? Trending { get; set; }

        public int? Status { get; set; }

        public double? NumberViews { get; set; }

        public double? Price { get; set; }

        [StringLength(50)]
        public string MetaKey { get; set; }

        public DateTime? Create_at { get; set; }

        public DateTime? Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
    }
}
