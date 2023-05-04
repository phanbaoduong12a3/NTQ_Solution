using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class ShippingModel
    {
        public int ID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? EndAt { get; set; }

        public int? OrderID { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public int? ShipMoney { get; set; }
        public string ShipMode { get; set; }
    }
}
