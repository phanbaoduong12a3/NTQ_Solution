namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipping")]
    public partial class Shipping
    {
        public int ID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? EndAt { get; set; }

        public int? OrderID { get; set; }
        public int? SupplierID { get; set; }
        public string Address { get; set; }
        public int? ShipMoney { get; set; } 
        public string ShipMode { get; set; }
    }
}
