namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public int? Status { get; set; }

        public DateTime? Create_at { get; set; }

        public DateTime? Update_at { get; set; }
    }
}
