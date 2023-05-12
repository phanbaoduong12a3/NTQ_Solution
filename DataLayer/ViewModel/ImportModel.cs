using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class ImportModel
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }
        public int? Count { get; set; }

        public double? Price { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? SupplierID { get; set; }

        public int? UserID { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        [DisplayName("Size")]
        public int? Color {get;set;}
        public int? Size{get;set;}
    }
}
