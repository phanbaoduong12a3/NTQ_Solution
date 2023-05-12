using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class ExportModel
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        public int? Count { get; set; }

        public double? Price { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? UserID { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public int? Color {get;set;}
        [DisplayName("Size")]
        public int? Size{get;set;}
        public double? ImportPrice { get; set; }
    }
}
