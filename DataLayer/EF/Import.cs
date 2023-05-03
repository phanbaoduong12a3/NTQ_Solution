namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Import")]
    public partial class Import
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        public int? Count { get; set; }

        public double? Price { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? SupplierID { get; set; }

        public int? UserID { get; set; }
    }
}
