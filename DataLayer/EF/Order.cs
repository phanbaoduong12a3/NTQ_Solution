namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }

        public int? ProductsID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "ntext")]
        public string Title { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public int? Status { get; set; }

        public int? Count { get; set; }

        public int? ShippingID { get; set; }

        public double? Price { get; set; }

        public int? Phone { get; set; }

        [StringLength(150)]
        public string Address { get; set; }
        public int? Color {get;set;}
        public int? Size{get;set;}
        public string ShipMode { get; set; }
    }
}
