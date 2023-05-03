namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string SupplierName { get; set; }

        public int? Phone { get; set; }

        [StringLength(150)]
        public string Address { get; set; }
    }
}
