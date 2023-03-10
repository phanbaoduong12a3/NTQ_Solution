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

        public int? NumberViews { get; set; }

        public double? Price { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }
    }
}
